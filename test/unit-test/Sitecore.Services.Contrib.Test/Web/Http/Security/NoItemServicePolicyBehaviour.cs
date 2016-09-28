using System;
using System.Web.Http;
using System.Web.Http.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.Services.Contrib.Web.Http.Security;
using Sitecore.Services.Core;
using Sitecore.Services.Core.ComponentModel.DataAnnotations;
using Sitecore.Services.Core.Diagnostics;
using Sitecore.Services.Core.Model;
using Sitecore.Services.Infrastructure.Services;
using Sitecore.Services.Infrastructure.Sitecore;
using Sitecore.Services.Infrastructure.Sitecore.Services;
using Sitecore.Services.Infrastructure.Web.Http.Security;

using Moq;
using Should;
using Xunit;

namespace Sitecore.Services.Contrib.Test.Web.Http.Security
{
    public class NoItemServicePolicyBehaviour
    {
        private readonly NoItemServicePolicy _policy;

        public NoItemServicePolicyBehaviour()
        {
            _policy = new NoItemServicePolicy();
        }

        [Fact]
        public void implements_IAuthorizePolicy()
        {
            typeof(IAuthorizePolicy)
                .IsAssignableFrom(typeof(NoItemServicePolicy))
                .ShouldBeTrue();
        }

        [Theory]
        [InlineData(typeof(ExampleApiController), true)]
        [InlineData(typeof(Infrastructure.Sitecore.Controllers.ItemServiceController), false)]
        [InlineData(typeof(ExampleEntityService), true)]
        public void verify_allowed_api_controller_types(Type controllerType, bool isAuthorised)
        {
            var serviceCollection =
                new ServiceCollection()
                    .AddScoped(controllerType)
                    .AddScoped(provider => new Mock<IHandlerProvider>().Object)
                    .AddScoped(provider => new Mock<ILogger>().Object)
                ;

            var serviceProvider = serviceCollection.BuildServiceProvider();

            DependencyInjection.ServiceLocator.SetServiceProvider(serviceProvider);

            var controlller = (IHttpController) DependencyInjection.ServiceLocator.ServiceProvider.GetService(controllerType);

            var httpActionContext = CreateActionContextForController(controlller);

            _policy.IsAuthorised(httpActionContext)
                   .ShouldEqual(isAuthorised);
        }

        private static HttpActionContext CreateActionContextForController(IHttpController controller)
        {
            var httpActionContext = new HttpActionContext
            {
                ControllerContext = new HttpControllerContext
                {
                    Controller = controller
                }
            };

            return httpActionContext;
        }
    }

    internal class ExampleApiController : ApiController
    {
    }

    internal class ExampleEntityService : EntityService<BusinessObject>
    {
        public ExampleEntityService()
            : base(new Mock<IRepository<BusinessObject>>().Object, new Mock<IMetaDataBuilder>().Object, new Mock<IEntityValidator>().Object)
        {
        }
    }

    public class BusinessObject : EntityIdentity
    {
    }
}
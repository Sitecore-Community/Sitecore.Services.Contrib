using System;
using System.Web.Http;
using System.Web.Http.Controllers;

using Sitecore.Services.Contrib.Web.Http.Security;
using Sitecore.Services.Core;
using Sitecore.Services.Core.Model;
using Sitecore.Services.Infrastructure.Sitecore.Services;
using Sitecore.Services.Infrastructure.Web.Http.Security;

using Moq;
using Should;
using Xunit;
using Xunit.Extensions;

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
            typeof(IAuthorizePolicy).IsAssignableFrom(typeof(NoItemServicePolicy)).ShouldBeTrue();
        }

        [Theory]
        [InlineData(typeof(ExampleApiController), true)]
        [InlineData(typeof(Infrastructure.Sitecore.Controllers.ItemServiceController), false)]
        [InlineData(typeof(ExampleEntityService), true)]
        public void verify_allowed_api_controller_types(Type controllerType, bool isAuthorised)
        {
            var controller = (IHttpController) Activator.CreateInstance(controllerType);
            var httpActionContext = CreateActionContextForController(controller);

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
        public ExampleEntityService(IRepository<BusinessObject> repository)
            : base(repository)
        {
        }

        public ExampleEntityService() : this(new Mock<IRepository<BusinessObject>>().Object)
        {
        }
    }

    public class BusinessObject : EntityIdentity
    {
    }
}
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

    [Fact]
    public void allows_api_controller()
    {
      var controller = new ExampleApiController();
      var httpActionContext = CreateActionContextForController(controller);

      var isAuthorised = _policy.IsAuthorised(httpActionContext);

      isAuthorised.ShouldBeTrue();
    }

    [Fact]
    public void disallows_itemservice_controller()
    {
      var controller = new Infrastructure.Sitecore.Controllers.ItemServiceController();
      var httpActionContext = CreateActionContextForController(controller);

      var isAuthorised = _policy.IsAuthorised(httpActionContext);

      isAuthorised.ShouldBeFalse();
    }

    [Fact]
    public void allows_an_entityservice_controller()
    {
      var repos = new Mock<IRepository<BusinessObject>>();
      var controller = new ExampleEntityService(repos.Object);

      var httpActionContext = CreateActionContextForController(controller);

      var isAuthorised = _policy.IsAuthorised(httpActionContext);

      isAuthorised.ShouldBeTrue();
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
  }

  public class BusinessObject : EntityIdentity
  {
  }
}
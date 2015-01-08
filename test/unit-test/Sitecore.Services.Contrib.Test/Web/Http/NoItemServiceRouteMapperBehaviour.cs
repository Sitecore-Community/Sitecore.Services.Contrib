using Should;
using Sitecore.Services.Contrib.Web.Http;
using Sitecore.Services.Infrastructure.Web.Http;
using Xunit;

namespace Sitecore.Services.Contrib.Test.Web.Http
{
    public class NoItemServiceRouteMapperBehaviour
    {
        [Fact]
        public void mapper_implements_IMapRoutes()
        {
            typeof(IMapRoutes).IsAssignableFrom(typeof(NoItemServiceRouteMapper)).ShouldBeTrue();
        }
    }
}
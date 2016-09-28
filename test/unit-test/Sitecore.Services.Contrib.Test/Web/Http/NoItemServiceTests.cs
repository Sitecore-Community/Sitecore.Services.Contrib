using Sitecore.Services.Contrib.Web.Http;
using Sitecore.Services.Core.Configuration;

namespace Sitecore.Services.Contrib.Test.Web.Http
{
    public class NoItemServiceTest
    {
        protected readonly NoItemServiceRouteMapper Sut;

        public NoItemServiceTest()
        {
            Sut = new NoItemServiceRouteMapper(new ConfigurationSettings());
        }
    }
}

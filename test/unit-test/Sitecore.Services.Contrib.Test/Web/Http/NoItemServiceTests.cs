using Sitecore.Services.Contrib.Web.Http;

namespace Sitecore.Services.Contrib.Test.Web.Http
{
  public class NoItemServiceTest
  {
    protected readonly NoItemServiceRouteMapper Sut;

    public NoItemServiceTest()
    {
      Sut = new NoItemServiceRouteMapper("");      
    }
  }
}

using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

using Sitecore.Services.Infrastructure.Sitecore.Configuration;
using Sitecore.Services.Infrastructure.Web.Http;

namespace Sitecore.Services.Contrib.Web.Http
{
  public class NoItemServiceRouteMapper : IMapRoutes
  {
    private readonly string _routeBase;

    public NoItemServiceRouteMapper(string routeBase)
    {
      _routeBase = routeBase;
    }

    /// <summary>
    /// Default constructor required to for CreateInstance in ConfigurationRouteConfigurationFactory to work
    /// </summary>
    public NoItemServiceRouteMapper()
      : this(RouteBaseSetting)
    {
    }

    public static string RouteBaseSetting
    {
      get { return new ServicesSettingsConfigurationProvider().Configuration.Services.Routes.RouteBase;  }
    }

    public void MapRoutes(HttpConfiguration config)
    {
      config.Routes.MapHttpRoute(
          DefaultRouteMapper.RouteName.EntityService.IdAction,
          _routeBase + "{namespace}/{controller}/{id}/{action}", 
          new { id = RouteParameter.Optional, action = "DefaultAction" }
          );
    }

    public void MapRoutes(RouteCollection routes)
    {
      routes.MapRoute(
          DefaultRouteMapper.RouteName.EntityService.MetaDataScript, 
          _routeBase + "script/metadata", 
          new { controller = "MetaDataScript", action = "GetScripts" }, 
          new[] { "Sitecore.Services.Infrastructure.Sitecore.Mvc" });
    }
  }
}
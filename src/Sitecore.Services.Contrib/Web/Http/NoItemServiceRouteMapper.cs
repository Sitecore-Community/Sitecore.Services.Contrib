using System.Web.Http;
using System.Web.Routing;

using Sitecore.Services.Infrastructure.Sitecore.Configuration;
using Sitecore.Services.Infrastructure.Web.Http;

namespace Sitecore.Services.Contrib.Web.Http
{
  public class NoItemServiceRouteMapper : IMapRoutes
  {
    private readonly IMapRoutes _routeMapper;

    public NoItemServiceRouteMapper(IMapRoutes routeMapper)
    {
      _routeMapper = routeMapper;
    }

    /// <summary>
    /// Default constructor required to for CreateInstance in ConfigurationRouteConfigurationFactory to work
    /// </summary>
    public NoItemServiceRouteMapper()
      : this(new DefaultRouteMapper(new ServicesSettingsConfigurationProvider().Configuration.Services.Routes.RouteBase))
    {
    }

    public void MapRoutes(HttpConfiguration config)
    {
      _routeMapper.MapRoutes(config);

      config.Routes.Remove(DefaultRouteMapper.RouteName.ItemService.Children);
      config.Routes.Remove(DefaultRouteMapper.RouteName.ItemService.ContentPath);
      config.Routes.Remove(DefaultRouteMapper.RouteName.ItemService.Default);
      config.Routes.Remove(DefaultRouteMapper.RouteName.ItemService.Path);
      config.Routes.Remove(DefaultRouteMapper.RouteName.ItemService.Query);
      config.Routes.Remove(DefaultRouteMapper.RouteName.ItemService.QueryViaItem);
      config.Routes.Remove(DefaultRouteMapper.RouteName.ItemService.Search);
      config.Routes.Remove(DefaultRouteMapper.RouteName.ItemService.SearchViaItem);
    }

    public void MapRoutes(RouteCollection routes)
    {
      _routeMapper.MapRoutes(routes);

      routes.Remove(routes[DefaultRouteMapper.RouteName.Authentication]);
    }
  }
}
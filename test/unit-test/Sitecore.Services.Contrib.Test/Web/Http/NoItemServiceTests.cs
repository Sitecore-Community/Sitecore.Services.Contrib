using System.Web.Http;
using System.Web.Routing;
using Sitecore.Services.Contrib.Web.Http;
using Sitecore.Services.Infrastructure.Web.Http;

using Should;
using Xunit;

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

  public class NoItemServiceRouteMapperBehaviour
  {
    [Fact]
    public void mapper_implements_IMapRoutes()
    {
      typeof(IMapRoutes).IsAssignableFrom(typeof(NoItemServiceRouteMapper)).ShouldBeTrue();
    }
  }

  public class NoItemServiceRouteMapperHttpConfigurationBehaviour : NoItemServiceTest
  {
    private readonly HttpConfiguration _httpConfiguration;
    private readonly RouteCollection _routeCollection;

    public NoItemServiceRouteMapperHttpConfigurationBehaviour()
    {
      _httpConfiguration = new HttpConfiguration();
      _routeCollection = new RouteCollection();

      Sut.MapRoutes(_httpConfiguration);
      Sut.MapRoutes(_routeCollection);
    }

    [Fact]
    public void entityservice_route_is_mapped()
    {
      _httpConfiguration.Routes.ContainsKey(DefaultRouteMapper.RouteName.EntityService.IdAction).ShouldBeTrue();
    }

    [Fact]
    public void entityservice_metadata_script_route_is_mapped()
    {
      _routeCollection[DefaultRouteMapper.RouteName.EntityService.MetaDataScript].ShouldNotBeNull();
    }

    [Fact]
    public void itemservice_query_via_item_route_not_mapped()
    {
      _httpConfiguration.Routes.ContainsKey(DefaultRouteMapper.RouteName.ItemService.QueryViaItem).ShouldBeFalse();
    }

    [Fact]
    public void itemservice_search_route_not_mapped()
    {
      _httpConfiguration.Routes.ContainsKey(DefaultRouteMapper.RouteName.ItemService.Search).ShouldBeFalse();
    }

    [Fact]
    public void itemservice_search_via_item_route_not_mapped()
    {
      _httpConfiguration.Routes.ContainsKey(DefaultRouteMapper.RouteName.ItemService.SearchViaItem).ShouldBeFalse();
    }

    [Fact]
    public void itemservice_children_route_not_mapped()
    {
      _httpConfiguration.Routes.ContainsKey(DefaultRouteMapper.RouteName.ItemService.Children).ShouldBeFalse();
    }

    [Fact]
    public void itemservice_default_route_not_mapped()
    {
      _httpConfiguration.Routes.ContainsKey(DefaultRouteMapper.RouteName.ItemService.Default).ShouldBeFalse();
    }

    [Fact]
    public void itemservice_content_path_route_not_mapped()
    {
      _httpConfiguration.Routes.ContainsKey(DefaultRouteMapper.RouteName.ItemService.ContentPath).ShouldBeFalse();
    }

    [Fact]
    public void itemservice_path_route_not_mapped()
    {
      _httpConfiguration.Routes.ContainsKey(DefaultRouteMapper.RouteName.ItemService.Path).ShouldBeFalse();
    }

    [Fact]
    public void authentication_route_not_mapped()
    {
      _routeCollection[DefaultRouteMapper.RouteName.Authentication].ShouldBeNull();
    }
  }
}

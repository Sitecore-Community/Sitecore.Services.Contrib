using System.Web.Http;
using System.Web.Routing;
using Should;
using Sitecore.Services.Infrastructure.Web.Http;
using Xunit.Extensions;

namespace Sitecore.Services.Contrib.Test.Web.Http
{
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

        [Theory]
        [InlineData(DefaultRouteMapper.RouteName.EntityService.IdAction, true)]
        [InlineData(DefaultRouteMapper.RouteName.ItemService.QueryViaItem, false)]
        [InlineData(DefaultRouteMapper.RouteName.ItemService.Search, false)]
        [InlineData(DefaultRouteMapper.RouteName.ItemService.SearchViaItem, false)]
        [InlineData(DefaultRouteMapper.RouteName.ItemService.Children, false)]
        [InlineData(DefaultRouteMapper.RouteName.ItemService.Default, false)]
        [InlineData(DefaultRouteMapper.RouteName.ItemService.ContentPath, false)]
        [InlineData(DefaultRouteMapper.RouteName.ItemService.Path, false)]
        public void verify_webapi_route_mapping(string routeKey, bool isMapped)
        {
            _httpConfiguration.Routes.ContainsKey(routeKey).ShouldEqual(isMapped);
        }

        [Theory]
        [InlineData(DefaultRouteMapper.RouteName.EntityService.MetaDataScript, true)]
        [InlineData(DefaultRouteMapper.RouteName.Authentication, false)]
        public void verify_mvc_route_mapping(string routeKey, bool isMapped)
        {
            (_routeCollection[routeKey] != null).ShouldEqual(isMapped);
        }
    }
}
using System;
using System.Web.Http;

using Sitecore.Services.Infrastructure.Sitecore.Configuration;
using Sitecore.Services.Infrastructure.Web.Http;

using Microsoft.Extensions.DependencyInjection;
using Sitecore.Services.Core.Configuration;

namespace Sitecore.Services.Contrib.Web.Http
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public class NoItemServiceRouteMapper : IMapRoutes
    {
        private readonly string _routeBase;

        public NoItemServiceRouteMapper(string routeBase)
        {
            _routeBase = routeBase;
        }

        public NoItemServiceRouteMapper(ConfigurationSettings configurationSettings)
        {
            if (configurationSettings == null)
            {
                throw new ArgumentNullException("configurationSettings");
            }

            _routeBase = configurationSettings.WebApi.Routes.RouteBase;
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
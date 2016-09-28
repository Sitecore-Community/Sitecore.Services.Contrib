using System;
using System.Web.Http.Controllers;

using Sitecore.Services.Infrastructure.Sitecore.Controllers;
using Sitecore.Services.Infrastructure.Web.Http.Security;

namespace Sitecore.Services.Contrib.Web.Http.Security
{
    public class NoItemServicePolicy : AuthorizationPolicyBase
    {
        public override bool IsAuthorised(HttpActionContext actionContext)
        {
            if (base.IsAuthorised(actionContext)) return true;

            return !IsItemService(actionContext.ControllerContext.Controller.GetType());
        }

        private static bool IsItemService(Type controllerType)
        {
            return typeof(ItemServiceController).IsAssignableFrom(controllerType);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace EComproj
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            // Keep FriendlyUrls (from the default template) if present
            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Permanent;
            routes.EnableFriendlyUrls(settings);

            // Route the site root "/" to the Catalog page
            routes.MapPageRoute("RootToCatalog", "", "~/Shop/Catalog.aspx");
        }
    }
}

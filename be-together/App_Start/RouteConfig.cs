// --------------------------------------------------------------------------------
// tailor-made software GmbH
// --------------------------------------------------------------------------------
// project:      be-together
// filename:     RouteConfig.cs
// --------------------------------------------------------------------------------
// 
// Created:      2015-01-06   08:47
// 
// Last changed: 2015-01-06   09:03
//               Torsten Vogt (tvo)
// --------------------------------------------------------------------------------

using System.Web.Mvc;
using System.Web.Routing;

namespace TMS.be_together
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            // Imprive SEO by stopping duplicate URL's due to case or trailing slashes.
            routes.AppendTrailingSlash = true;
            routes.LowercaseUrls = true;

            // IgnoreRoute - Tell the routing system to ignore certain routes for better performance.
            // Ignore .axd files.
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            // Ignore everything in the Content folder.
            routes.IgnoreRoute("Content/{*pathInfo}");
            // Ignore everything in the Scripts folder.
            routes.IgnoreRoute("Scripts/{*pathInfo}");
            // Ignore the humans.txt file.
            routes.IgnoreRoute("humans.txt");
            // Ignore the elmah route.
            routes.IgnoreRoute("elmah");
            // Ignore the glimpse route.
            routes.IgnoreRoute("glimpse");

            // Enable attribute routing.
            routes.MapMvcAttributeRoutes();

            // Normal routes have been removed in favour of attribute routing. Here is an example of the default one.
            // routes.MapRoute(
            //     name: "Default",
            //     url: "{controller}/{action}/{id}",
            //     defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}
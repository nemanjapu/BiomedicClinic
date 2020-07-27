using BiomedicClinic.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BiomedicClinic
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();
            routes.LowercaseUrls = true;

            routes.MapRoute(
                "Account",
                "account/{controller}/{action}",
                new { controller = "Account", action = "Login" }
            );

            routes.MapRoute(
                "Leads",
                "leads/{controller}/{action}",
                new { controller = "Contact", action = "Index" }
            );

            routes.MapRoute(
                "ImagesEditor",
                "imageseditor/{controller}/{action}",
                new { controller = "ImagesManager", action = "SelectImage" }
            );

            routes.MapRoute(
                name: "Editor",
                url: "editor/{*url}",
                defaults: new
                {
                    controller = "CMSEditor",
                    action = "ReturnCMSPageEdit",
                    url = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "CMSroute",
                url: "{*url}",
                defaults: new
                {
                    controller = "CMS",
                    action = "ReturnCMSPage",
                    url = UrlParameter.Optional
                },
                constraints: new { url = new CmsUrlConstraint() }
            );

            routes.MapRoute(
               "Sitemap",
               "sitemap.xml",
               new { controller = "CMS", action = "SitemapXml" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

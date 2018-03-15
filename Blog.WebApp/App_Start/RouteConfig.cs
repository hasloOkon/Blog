﻿using System.Web.Mvc;
using System.Web.Routing;

namespace Blog.WebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "AddPost",
                url: "AddPost",
                defaults: new { controller = "Admin", action = "AddPost" }
            );

            routes.MapRoute(
                name: "Logout",
                url: "Logout",
                defaults: new { controller = "Admin", action = "Logout" }
            );

            routes.MapRoute(
                name: "Login",
                url: "Login",
                defaults: new { controller = "Admin", action = "Login" }
            );

            routes.MapRoute(
                name: "Manage",
                url: "Manage",
                defaults: new { controller = "Admin", action = "Manage" }
            );

            routes.MapRoute(
                name: "PostDetails",
                url: "PostDetails/{year}/{month}/{postSlug}",
                defaults: new { controller = "Blog", action = "PostDetails" },
                constraints: new { year = @"\d{4}", month = @"\d{1,2}" }
            );

            routes.MapRoute(
                name: "Category",
                url: "Category/{categorySlug}",
                defaults: new { controller = "Blog", action = "Category" }
            );

            routes.MapRoute(
                name: "Tag",
                url: "Tag/{tagSlug}",
                defaults: new { controller = "Blog", action = "Tag" }
            );

            routes.MapRoute(
                name: "Action",
                url: "{action}",
                defaults: new { controller = "Blog", action = "Posts" }
            );
        }
    }
}

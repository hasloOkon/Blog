using System.Web.Mvc;
using System.Web.Routing;

namespace Blog.WebApp
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute
            (
                name: "UnexpectedError",
                url: "UnexpectedError",
                defaults: new { controller = "Error", action = "UnexpectedError" }
            );

            routes.MapRoute
            (
                name: "NotFound",
                url: "NotFound",
                defaults: new { controller = "Error", action = "NotFound" }
            );

            routes.MapRoute
            (
                name: "Admin",
                url: "Admin/{action}",
                defaults: new { controller = "Admin", action = "Manage" }
            );

            routes.MapRoute
            (
                name: "Logout",
                url: "Logout",
                defaults: new { controller = "Admin", action = "Logout" }
            );

            routes.MapRoute
            (
                name: "Login",
                url: "Login",
                defaults: new { controller = "Admin", action = "Login" }
            );

            routes.MapRoute
            (
                name: "PostDetails",
                url: "PostDetails/{year}/{month}/{postSlug}",
                defaults: new { controller = "Blog", action = "PostDetails" },
                constraints: new { year = @"\d{4}", month = @"\d{1,2}" }
            );

            routes.MapRoute
            (
                name: "Category",
                url: "Category/{categorySlug}",
                defaults: new { controller = "Blog", action = "Category" }
            );

            routes.MapRoute
            (
                name: "Tag",
                url: "Tag/{tagSlug}",
                defaults: new { controller = "Blog", action = "Tag" }
            );

            routes.MapRoute
            (
                name: "Action",
                url: "{action}",
                defaults: new { controller = "Blog", action = "Posts" }
            );
        }
    }
}

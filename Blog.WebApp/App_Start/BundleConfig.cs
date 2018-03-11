using System.Web.Optimization;

namespace Blog.WebApp
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            var styleBundle = new StyleBundle("~/night_sky_2/css")
                .Include("~/Content/themes/night_sky_2/style.css")
                .Include("~/Content/themes/night_sky_2/admin.css");

            bundles.Add(styleBundle);
        }
    }
}
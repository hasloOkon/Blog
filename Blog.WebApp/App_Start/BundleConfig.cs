using System.Runtime.InteropServices;
using System.Web.Optimization;

namespace Blog.WebApp
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterStyleBundles(bundles);
            RegisterScriptBundles(bundles);
        }

        private static void RegisterScriptBundles(BundleCollection bundles)
        {
            var externalScripts = new ScriptBundle("~/external/js")
                .IncludeDirectory("~/Scripts/external", "*.js", searchSubdirectories: true);

            bundles.Add(externalScripts);
        }

        private static void RegisterStyleBundles(BundleCollection bundles)
        {
            var externalStyles = new StyleBundle("~/external/css")
                .IncludeDirectory("~/Content/external", "*.css", searchSubdirectories: true);

            bundles.Add(externalStyles);

            var internalStyles = new StyleBundle("~/internal/css")
                .Include("~/Content/themes/night_sky_2/style.css")
                .Include("~/Content/themes/night_sky_2/admin.css");

            bundles.Add(internalStyles);
        }
    }
}
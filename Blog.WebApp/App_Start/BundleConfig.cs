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
                .Include("~/Scripts/external/jquery-{version}.js")
                .Include("~/Scripts/external/jquery-ui.js")
                .Include("~/Scripts/external/tether.js")
                .Include("~/Scripts/external/popper.js")
                .Include("~/Scripts/external/bootstrap.js")
                .Include("~/Scripts/external/tooltip.js")
                .Include("~/Scripts/external/summernote-bs4.js")
                .Include("~/Scripts/external/sumernote-fixedtoolbar.js");

            bundles.Add(externalScripts);
        }

        private static void RegisterStyleBundles(BundleCollection bundles)
        {
            var externalStyles = new StyleBundle("~/external/css")
                .IncludeDirectory("~/Content/external", "*.css", searchSubdirectories: true);

            bundles.Add(externalStyles);

            var internalStyles = new StyleBundle("~/internal/css")
                .Include("~/Content/themes/orange/style.css");

            bundles.Add(internalStyles);
        }
    }
}

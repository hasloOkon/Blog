using Blog.Core;
using Ninject;
using Ninject.Web.Common;
using System.Web.Optimization;
using System.Web.Routing;

namespace Blog.WebApp
{
    public class MvcApplication : NinjectHttpApplication
    {
        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            kernel.Load(new CoreModule());
            kernel.Load(new WebModule());

            return kernel;
        }

        protected override void OnApplicationStarted()
        {
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            MapperConfig.CreateMaps();
            base.OnApplicationStarted();
        }
    }
}

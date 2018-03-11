using System.Web.Optimization;
using System.Web.Routing;
using Blog.Core;
using Blog.WebApp.ViewModels;
using Ninject;
using Ninject.Web.Common;

namespace Blog.WebApp
{
    public class MvcApplication : NinjectHttpApplication
    {
        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            kernel.Load(new RepositoryModule());
            kernel.Bind<IViewModelFactory>().To<ViewModelFactory>();

            return kernel;
        }

        protected override void OnApplicationStarted()
        {
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            base.OnApplicationStarted();
        }
    }
}

using System.Linq;
using System.Reflection;
using Blog.Core;
using Blog.WebApp.Providers;
using Blog.WebApp.ViewModels;
using Ninject;
using Ninject.Web.Common;
using System.Web.Optimization;
using System.Web.Routing;
using Blog.Core.Utility;

namespace Blog.WebApp
{
    public class MvcApplication : NinjectHttpApplication
    {
        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            kernel.Load(new CoreModule());
            kernel.Bind<IViewModelFactory>().To<ViewModelFactory>();

            Assembly.GetAssembly(typeof(MvcApplication))
                .GetTypes()
                .Where(type => type.Name.EndsWith("Provider") && type.IsClass && !type.IsAbstract)
                .ForEach(type => kernel.Bind(type.GetInterface("I" + type.Name)).To(type));

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

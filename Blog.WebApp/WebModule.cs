using Blog.Core.Utility;
using Ninject.Modules;

namespace Blog.WebApp
{
    public class WebModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.BindManyByName("Factory");
            Kernel.BindManyByName("Provider");
        }
    }
}
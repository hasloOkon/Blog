using System.Linq;
using System.Reflection;
using Ninject;

namespace Blog.Core.Utility
{
    public static class KernelExtensions
    {
        public static void BindManyByName(this IKernel kernel, string nameSuffix)
        {
            Assembly.GetCallingAssembly()
                .GetTypes()
                .Where(type => type.Name.EndsWith(nameSuffix) && type.IsClass && !type.IsAbstract)
                .ForEach(type => kernel.Bind(type.GetInterface("I" + type.Name)).To(type));
        }
    }
}
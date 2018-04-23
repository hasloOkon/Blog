using Ninject;
using System.Linq;
using System.Reflection;

namespace Blog.Core.Utility
{
    public static class KernelExtensions
    {
        public static void BindManyByName(this IKernel kernel, string nameSuffix)
        {
            Assembly.GetCallingAssembly()
                .GetNonAbstractClasses()
                .Where(type => type.Name.EndsWith(nameSuffix))
                .ForEach(type => kernel.Bind(type.GetInterface("I" + type.Name)).To(type));
        }
    }
}
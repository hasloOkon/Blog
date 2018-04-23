using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Blog.Core.Utility
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<Type> GetNonAbstractClasses(this Assembly assembly)
        {
            return assembly.GetTypes().Where(type => type.IsClass && !type.IsAbstract);
        }
    }
}

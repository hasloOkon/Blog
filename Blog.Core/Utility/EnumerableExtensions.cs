using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.Utility
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
            }
        }

        public static void ForEachAsync<T>(this IEnumerable<T> items, Action<T> action)
        {
            var tasks = items
                .Select(item => Task.Run(() => action(item)))
                .ToArray();

            Task.WaitAll(tasks);
        }
    }
}
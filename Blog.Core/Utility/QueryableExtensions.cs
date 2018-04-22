using System.Linq;

namespace Blog.Core.Utility
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Page<T>(this IQueryable<T> queryable, int pageNumber, int pageSize)
        {
            return queryable
                .Skip((pageNumber - 1)*pageSize)
                .Take(pageSize);
        }
    }
}
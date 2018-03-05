using System.Collections.Generic;
using System.Linq;
using Blog.Core.DomainObjects;
using NHibernate;
using NHibernate.Linq;

namespace Blog.Core
{
    public class BlogRepository : IBlogRepository
    {
        private readonly ISession session;

        public BlogRepository(ISession session)
        {
            this.session = session;
        }

        public IList<Post> Posts(int pageNumber, int pageSize)
        {
            var posts = session.Query<Post>()
                                  .Where(p => p.Published)
                                  .OrderByDescending(p => p.PostedOn)
                                  .Skip((pageNumber - 1) * pageSize)
                                  .Take(pageSize)
                                  .Fetch(p => p.Category)
                                  .ToList();

            var postIds = posts.Select(p => p.Id).ToList();

            return session.Query<Post>()
                  .Where(p => postIds.Contains(p.Id))
                  .OrderByDescending(p => p.PostedOn)
                  .FetchMany(p => p.Tags)
                  .ToList();
        }

        public int TotalPosts()
        {
            return session.Query<Post>().Where(p => p.Published).Count();
        }
    }
}

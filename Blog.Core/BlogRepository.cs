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
                                  .Where(post => post.Published)
                                  .OrderByDescending(post => post.PostedOn)
                                  .Skip((pageNumber - 1) * pageSize)
                                  .Take(pageSize)
                                  .Fetch(post => post.Category)
                                  .ToList();

            var postIds = posts.Select(post => post.Id).ToList();

            return session.Query<Post>()
                  .Where(post => postIds.Contains(post.Id))
                  .OrderByDescending(post => post.PostedOn)
                  .FetchMany(post => post.Tags)
                  .ToList();
        }

        public int TotalPosts()
        {
            return session.Query<Post>().Count(post => post.Published);
        }

        public IList<Post> PostsForCategory(string categorySlug, int pageNumber, int pageSize)
        {
            var posts = session.Query<Post>()
                                  .Where(post => post.Published && post.Category.UrlSlug == categorySlug)
                                  .OrderByDescending(post => post.PostedOn)
                                  .Skip((pageNumber - 1) * pageSize)
                                  .Take(pageSize)
                                  .Fetch(post => post.Category)
                                  .ToList();

            var postIds = posts.Select(post => post.Id).ToList();

            return session.Query<Post>()
                  .Where(post => postIds.Contains(post.Id))
                  .OrderByDescending(post => post.PostedOn)
                  .FetchMany(post => post.Tags)
                  .ToList();
        }

        public int TotalPostsForCategory(string categorySlug)
        {
            return session.Query<Post>().Count(post => post.Published && post.Category.UrlSlug == categorySlug);
        }

        public Category Category(string categorySlug)
        {
            return session.Query<Category>().FirstOrDefault(category => category.UrlSlug == categorySlug);
        }

        public IList<Post> PostsForTag(string tagSlug, int pageNumber, int pageSize)
        {
            var posts = session.Query<Post>()
                                  .Where(post => post.Published && post.Tags.Any(tag => tag.UrlSlug == tagSlug))
                                  .OrderByDescending(post => post.PostedOn)
                                  .Skip((pageNumber - 1) * pageSize)
                                  .Take(pageSize)
                                  .Fetch(post => post.Category)
                                  .ToList();

            var postIds = posts.Select(post => post.Id).ToList();

            return session.Query<Post>()
                  .Where(post => postIds.Contains(post.Id))
                  .OrderByDescending(post => post.PostedOn)
                  .FetchMany(post => post.Tags)
                  .ToList();
        }

        public int TotalPostsForTag(string tagSlug)
        {
            return session.Query<Post>().Count(post => post.Published && post.Tags.Any(tag => tag.UrlSlug == tagSlug));
        }

        public Tag Tag(string tagSlug)
        {
            return session.Query<Tag>().FirstOrDefault(tag => tag.UrlSlug == tagSlug);
        }
    }
}

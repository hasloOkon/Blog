using Blog.Core.Models;
using NHibernate;
using NHibernate.Linq;
using System.Collections.Generic;
using System.Linq;
using Blog.Core.Utility;

namespace Blog.Core.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(ISession session)
            : base(session)
        {
        }

        public IList<Post> Posts(int pageNumber, int pageSize)
        {
            var posts = Session.Query<Post>()
                                  .OrderByDescending(post => post.PostedOn)
                                  .Page(pageNumber, pageSize)
                                  .Fetch(post => post.Category)
                                  .ToList();

            var postIds = posts.Select(post => post.Id).ToList();

            return Session.Query<Post>()
                  .Where(post => postIds.Contains(post.Id))
                  .OrderByDescending(post => post.PostedOn)
                  .FetchMany(post => post.Tags)
                  .ToList();
        }

        public int TotalPosts()
        {
            return Session.Query<Post>().Count();
        }

        public IList<Post> PostsForCategory(string categorySlug, int pageNumber, int pageSize)
        {
            var posts = Session.Query<Post>()
                                  .Where(post => post.Category.UrlSlug == categorySlug)
                                  .OrderByDescending(post => post.PostedOn)
                                  .Page(pageNumber, pageSize)
                                  .Fetch(post => post.Category)
                                  .ToList();

            var postIds = posts.Select(post => post.Id).ToList();

            return Session.Query<Post>()
                  .Where(post => postIds.Contains(post.Id))
                  .OrderByDescending(post => post.PostedOn)
                  .FetchMany(post => post.Tags)
                  .ToList();
        }

        public int TotalPostsForCategory(string categorySlug)
        {
            return Session.Query<Post>().Count(post => post.Category.UrlSlug == categorySlug);
        }

        public IList<Post> PostsForTag(string tagSlug, int pageNumber, int pageSize)
        {
            var posts = Session.Query<Post>()
                                  .Where(post => post.Tags.Any(tag => tag.UrlSlug == tagSlug))
                                  .OrderByDescending(post => post.PostedOn)
                                  .Page(pageNumber, pageSize)
                                  .Fetch(post => post.Category)
                                  .ToList();

            var postIds = posts.Select(post => post.Id).ToList();

            return Session.Query<Post>()
                  .Where(post => postIds.Contains(post.Id))
                  .OrderByDescending(post => post.PostedOn)
                  .FetchMany(post => post.Tags)
                  .ToList();
        }

        public int TotalPostsForTag(string tagSlug)
        {
            return Session.Query<Post>().Count(post => post.Tags.Any(tag => tag.UrlSlug == tagSlug));
        }

        public IList<Post> PostsBySearch(string searchPhrase, int pageNumber, int pageSize)
        {
            searchPhrase = searchPhrase.ToLower();

            var posts = Session.Query<Post>()
                .Where(post => post.Title.ToLower().Contains(searchPhrase) || post.Content.ToLower().Contains(searchPhrase))
                .OrderByDescending(post => post.PostedOn)
                .Page(pageNumber, pageSize)
                .Fetch(post => post.Category)
                .ToList();

            var postIds = posts.Select(post => post.Id).ToList();

            return Session.Query<Post>()
                  .Where(post => postIds.Contains(post.Id))
                  .OrderByDescending(post => post.PostedOn)
                  .FetchMany(post => post.Tags)
                  .ToList();
        }

        public int TotalPostsBySearch(string searchPhrase)
        {
            searchPhrase = searchPhrase.ToLower();

            return Session.Query<Post>()
                .Count(post => post.Title.ToLower().Contains(searchPhrase) || post.Content.ToLower().Contains(searchPhrase));
        }

        public Post PostDetails(int year, int month, string postSlug)
        {
            return Session.Query<Post>()
                .Fetch(post => post.Category)
                .FirstOrDefault(post => post.PostedOn.Year == year
                    && post.PostedOn.Month == month
                    && post.UrlSlug == postSlug);
        }
    }
}

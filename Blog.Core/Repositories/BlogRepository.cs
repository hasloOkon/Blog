using Blog.Core.Models;
using NHibernate;
using NHibernate.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Core.Repositories
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

        public IList<Post> PostsBySearch(string searchPhrase, int pageNumber, int pageSize)
        {
            searchPhrase = searchPhrase.ToLower();

            var posts = session.Query<Post>()
                .Where(post => post.Published
                    && post.Title.ToLower().Contains(searchPhrase) || post.Content.ToLower().Contains(searchPhrase))
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

        public int TotalPostsBySearch(string searchPhrase)
        {
            searchPhrase = searchPhrase.ToLower();

            return session.Query<Post>().Count(post => post.Published
                && post.Title.ToLower().Contains(searchPhrase) || post.Content.ToLower().Contains(searchPhrase));
        }

        public Post PostDetails(int year, int month, string postSlug)
        {
            return session.Query<Post>()
                .Fetch(post => post.Category)
                .FirstOrDefault(post => post.Published
                    && post.PostedOn.Year == year
                    && post.PostedOn.Month == month
                    && post.UrlSlug == postSlug);
        }

        public IList<Category> Categories()
        {
            return session.Query<Category>().ToList();
        }

        public IList<Tag> Tags()
        {
            return session.Query<Tag>().ToList();
        }

        public void AddOrUpdatePost(Post post)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.SaveOrUpdate(post);
                transaction.Commit();
            }
        }

        public void AddCategory(Category category)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Save(category);
                transaction.Commit();
            }
        }

        public void AddTag(Tag tag)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Save(tag);
                transaction.Commit();
            }
        }

        public Post GetPostById(int id)
        {
            return session.Query<Post>().First(post => post.Id == id);
        }
    }
}

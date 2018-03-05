using System;
using System.Web;
using System.Web.Mvc;
using Blog.Core;
using Blog.WebApp.Models;

namespace Blog.WebApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository blogRepository;

        public BlogController(IBlogRepository blogRepository)
        {
            this.blogRepository = blogRepository;
        }

        public ViewResult Posts(int pageNumber = 1)
        {
            pageNumber = Math.Max(1, pageNumber);

            var posts = blogRepository.Posts(pageNumber, pageSize: 10);

            var totalPosts = blogRepository.TotalPosts();

            var postsViewModel = new PostsViewModel
            {
                Posts = posts,
                TotalPosts = totalPosts
            };

            ViewBag.Title = "Latest Posts";

            return View("Posts", postsViewModel);
        }

        public ViewResult Category(string categorySlug, int pageNumber = 1)
        {
            pageNumber = Math.Max(1, pageNumber);

            var category = blogRepository.Category(categorySlug);

            if (category == null)
                throw new HttpException(404, "Category not found");

            var posts = blogRepository.PostsForCategory(categorySlug, pageNumber, pageSize: 10);
            var totalPosts = blogRepository.TotalPostsForCategory(categorySlug);

            var postsViewModel = new PostsViewModel
            {
                Posts = posts,
                TotalPosts = totalPosts,
            };

            ViewBag.Title = $"Latest Posts for category \"{category.Name}\"";

            return View("Posts", postsViewModel);
        }

        public ViewResult Tag(string tagSlug, int pageNumber = 1)
        {
            pageNumber = Math.Max(1, pageNumber);

            var tag = blogRepository.Tag(tagSlug);

            if (tag == null)
                throw new HttpException(404, "Tag not found");

            var posts = blogRepository.PostsForTag(tagSlug, pageNumber, pageSize: 10);
            var totalPosts = blogRepository.TotalPostsForTag(tagSlug);

            var postsViewModel = new PostsViewModel
            {
                Posts = posts,
                TotalPosts = totalPosts
            };

            ViewBag.Title = $"Lastest Posts for tag \"{tag.Name}\"";

            return View("Posts", postsViewModel);
        }
    }
}
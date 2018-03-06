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

            var postsViewModel = new PostsViewModel
            {
                Posts = blogRepository.Posts(pageNumber, pageSize: 10),
                TotalPosts = blogRepository.TotalPosts()
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

            var postsViewModel = new PostsViewModel
            {
                Posts = blogRepository.PostsForCategory(categorySlug, pageNumber, pageSize: 10),
                TotalPosts = blogRepository.TotalPostsForCategory(categorySlug)
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

            var postsViewModel = new PostsViewModel
            {
                Posts = blogRepository.PostsForTag(tagSlug, pageNumber, pageSize: 10),
                TotalPosts = blogRepository.TotalPostsForTag(tagSlug)
            };

            ViewBag.Title = $"Lastest Posts for tag \"{tag.Name}\"";

            return View("Posts", postsViewModel);
        }

        public ViewResult Search(string searchPhrase, int pageNumber = 1)
        {
            pageNumber = Math.Max(1, pageNumber);

            var postsViewModel = new PostsViewModel
            {
                Posts = blogRepository.PostsBySearch(searchPhrase, pageNumber, pageSize: 10),
                TotalPosts = blogRepository.TotalPostsBySearch(searchPhrase)
            };

            ViewBag.Title = $"Search results for phrase \"{searchPhrase}\"";

            return View("Posts", postsViewModel);
        }

        public ViewResult PostDetails(int year, int month, string postSlug)
        {
            var postDetails = blogRepository.PostDetails(year, month, postSlug);

            return View("PostDetails", postDetails);
        }

        [ChildActionOnly]
        public PartialViewResult Sidebar()
        {
            var sidebarViewModel = new SidebarViewModel
            {
                Categories = blogRepository.Categories()
            };

            return PartialView("_Sidebar", sidebarViewModel);
        }
    }
}
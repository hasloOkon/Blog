using Blog.WebApp.Aspects;
using Blog.WebApp.ViewModels;
using System;
using System.Web.Mvc;

namespace Blog.WebApp.Controllers
{
    [AllowAnonymous]
    [ProfileController]
    public class BlogController : Controller
    {
        private readonly IViewModelFactory viewModelFactory;

        public BlogController(IViewModelFactory viewModelFactory)
        {
            this.viewModelFactory = viewModelFactory;
        }

        [HttpGet]
        public ViewResult Posts(int pageNumber = 1)
        {
            pageNumber = Math.Max(1, pageNumber);

            var postsViewModel = viewModelFactory.GetPosts(pageNumber);
            ViewBag.Title = postsViewModel.Title;

            return View("Posts", postsViewModel);
        }

        [HttpGet]
        public ViewResult Category(string categorySlug, int pageNumber = 1)
        {
            pageNumber = Math.Max(1, pageNumber);

            var postsViewModel = viewModelFactory.GetPostsForCategory(categorySlug, pageNumber);
            ViewBag.Title = postsViewModel.Title;

            return View("Posts", postsViewModel);
        }

        [HttpGet]
        public ViewResult Tag(string tagSlug, int pageNumber = 1)
        {
            pageNumber = Math.Max(1, pageNumber);

            var postsViewModel = viewModelFactory.GetPostsForTag(tagSlug, pageNumber);
            ViewBag.Title = postsViewModel.Title;

            return View("Posts", postsViewModel);
        }

        [HttpGet]
        public ViewResult Search(string searchPhrase, int pageNumber = 1)
        {
            pageNumber = Math.Max(1, pageNumber);

            var postsViewModel = viewModelFactory.GetPostsBySearch(searchPhrase, pageNumber);
            ViewBag.Title = postsViewModel.Title;

            return View("Posts", postsViewModel);
        }

        [HttpGet]
        public ViewResult PostDetails(int year, int month, string postSlug)
        {
            var postDetails = viewModelFactory.GetPostDetails(year, month, postSlug);

            return View("PostDetails", postDetails);
        }

        [HttpGet]
        public ViewResult About()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult LeftSidebar()
        {
            var sidebarViewModel = viewModelFactory.GetLeftSidebar();

            return PartialView("_LeftSidebar", sidebarViewModel);
        }
    }
}

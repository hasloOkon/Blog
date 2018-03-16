using System;
using System.Web.Mvc;
using Blog.WebApp.ViewModels;

namespace Blog.WebApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly IViewModelFactory viewModelFactory;

        public BlogController(IViewModelFactory viewModelFactory)
        {
            this.viewModelFactory = viewModelFactory;
        }

        public ViewResult Posts(int pageNumber = 1)
        {
            pageNumber = Math.Max(1, pageNumber);

            var postsViewModel = viewModelFactory.GetPosts(pageNumber);
            ViewBag.Title = postsViewModel.Title;

            return View("Posts", postsViewModel);
        }

        public ViewResult Category(string categorySlug, int pageNumber = 1)
        {
            pageNumber = Math.Max(1, pageNumber);

            var postsViewModel = viewModelFactory.GetPostsForCategory(categorySlug, pageNumber);
            ViewBag.Title = postsViewModel.Title;

            return View("Posts", postsViewModel);
        }

        public ViewResult Tag(string tagSlug, int pageNumber = 1)
        {
            pageNumber = Math.Max(1, pageNumber);

            var postsViewModel = viewModelFactory.GetPostsForTag(tagSlug, pageNumber);
            ViewBag.Title = postsViewModel.Title;

            return View("Posts", postsViewModel);
        }

        public ViewResult Search(string searchPhrase, int pageNumber = 1)
        {
            pageNumber = Math.Max(1, pageNumber);

            var postsViewModel = viewModelFactory.GetPostsBySearch(searchPhrase, pageNumber);
            ViewBag.Title = postsViewModel.Title;

            return View("Posts", postsViewModel);
        }

        public ViewResult PostDetails(int year, int month, string postSlug)
        {
            var postDetails = viewModelFactory.GetPostDetails(year, month, postSlug);

            return View("PostDetails", postDetails);
        }

        [ChildActionOnly]
        public PartialViewResult LeftSidebar()
        {
            var sidebarViewModel = viewModelFactory.GetLeftSidebar();

            return PartialView("_LeftSidebar", sidebarViewModel);
        }
    }
}

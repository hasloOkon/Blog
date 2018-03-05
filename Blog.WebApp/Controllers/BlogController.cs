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
    }
}
using Blog.Core;
using Blog.Core.Models;
using Blog.Core.Utility;
using Blog.WebApp.Providers;
using Blog.WebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Blog.WebApp.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ILoginProvider loginProvider;
        private readonly IBlogRepository blogRepository;

        public AdminController(ILoginProvider loginProvider, IBlogRepository blogRepository)
        {
            this.loginProvider = loginProvider;
            this.blogRepository = blogRepository;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (loginProvider.IsLoggedIn)
            {
                return RedirectToUrl(returnUrl);
            }

            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (loginProvider.Login(model.UserName, model.Password))
                {
                    return RedirectToUrl(returnUrl);
                }

                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }
            return View();
        }

        public ActionResult Logout()
        {
            loginProvider.Logout();

            return RedirectToAction("Login", "Admin");
        }

        public ActionResult Manage()
        {
            return View();
        }

        public ActionResult AddPost()
        {
            return View(new AddPostForm
            {
                Categories = blogRepository.Categories(),
                Tags = blogRepository.Tags()
            });
        }

        [HttpPost]
        public ActionResult AddPost(AddPostForm addPostForm)
        {
            addPostForm.TagIds = addPostForm.TagIds ?? new List<int>();

            if (ModelState.IsValid)
            {
                var post = new Post
                {
                    Title = addPostForm.Title,
                    Content = addPostForm.Content,
                    Published = addPostForm.Published,
                    Category = blogRepository
                        .Categories()
                        .First(category => category.Id == addPostForm.CategoryId),
                    Tags = blogRepository
                        .Tags()
                        .Where(tag => addPostForm.TagIds.Contains(tag.Id)).ToList(),
                    ShortDescription = addPostForm.ShortDescription,
                    PostedOn = DateTime.Now,
                    Meta = "test meta",
                    UrlSlug = addPostForm.Title.Slugify()
                };

                blogRepository.AddPost(post);

                return RedirectToAction("Posts", "Blog");
            }
            else
            {
                addPostForm.Categories = blogRepository.Categories();
                addPostForm.Tags = blogRepository.Tags();

                return View(addPostForm);
            }
        }

        public ActionResult AddCategory()
        {
            return View(new AddCategoryForm());
        }

        [HttpPost]
        public ActionResult AddCategory(AddCategoryForm addCategoryForm)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    Name = addCategoryForm.Name,
                    Description = addCategoryForm.Description,
                    UrlSlug = addCategoryForm.Name.Slugify()
                };

                blogRepository.AddCategory(category);

                return RedirectToAction("Posts", "Blog");
            }
            else
            {
                return View(addCategoryForm);
            }
        }

        private ActionResult RedirectToUrl(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Manage");
            }
        }
    }
}
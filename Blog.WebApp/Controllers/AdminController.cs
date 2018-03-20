using Blog.Core.Models;
using Blog.Core.Repositories;
using Blog.Core.Utility;
using Blog.WebApp.Providers;
using Blog.WebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.WebApp.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ILoginProvider loginProvider;
        private readonly IImageProvider imageProvider;
        private readonly IBlogRepository blogRepository;
        private readonly IViewModelFactory viewModelFactory;

        public AdminController(ILoginProvider loginProvider, IImageProvider imageProvider,
            IBlogRepository blogRepository, IViewModelFactory viewModelFactory)
        {
            this.loginProvider = loginProvider;
            this.imageProvider = imageProvider;
            this.blogRepository = blogRepository;
            this.viewModelFactory = viewModelFactory;
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

        public ActionResult Images()
        {
            return View(viewModelFactory.GetImages());
        }

        [HttpPost]
        public ActionResult AddImage(HttpPostedFileBase image)
        {
            if (image != null)
            {
                imageProvider.SavePostedImage(image, Server);
            }

            return RedirectToAction("Images");
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

                blogRepository.AddOrUpdatePost(post);

                return RedirectToAction("Posts", "Blog");
            }
            else
            {
                addPostForm.Categories = blogRepository.Categories();
                addPostForm.Tags = blogRepository.Tags();

                return View(addPostForm);
            }
        }

        public ActionResult EditPost(int postId)
        {
            var post = blogRepository.GetPostById(postId);

            return View(new EditPostForm
            {
                Id = postId,
                Title = post.Title,
                Content = post.Content,
                Published = post.Published,
                CategoryId = post.Category.Id,
                TagIds = post.Tags.Select(tag => tag.Id).ToList(),
                ShortDescription = post.ShortDescription,

                Categories = blogRepository.Categories(),
                Tags = blogRepository.Tags()
            });
        }

        [HttpPost]
        public ActionResult EditPost(EditPostForm editPostForm)
        {
            editPostForm.TagIds = editPostForm.TagIds ?? new List<int>();

            if (ModelState.IsValid)
            {
                var post = blogRepository.GetPostById(editPostForm.Id);

                post.Title = editPostForm.Title;
                post.Content = editPostForm.Content;
                post.Published = editPostForm.Published;
                post.Category = blogRepository
                    .Categories()
                    .First(category => category.Id == editPostForm.CategoryId);
                post.Tags = blogRepository
                    .Tags()
                    .Where(tag => editPostForm.TagIds.Contains(tag.Id)).ToList();
                post.ShortDescription = editPostForm.ShortDescription;
                post.Modified = DateTime.Now;
                post.Meta = "test meta";
                post.UrlSlug = editPostForm.Title.Slugify();

                blogRepository.AddOrUpdatePost(post);

                return RedirectToAction("Posts", "Blog");
            }
            else
            {
                editPostForm.Categories = blogRepository.Categories();
                editPostForm.Tags = blogRepository.Tags();

                return View(editPostForm);
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

                blogRepository.AddOrUpdateCategory(category);

                return RedirectToAction("Posts", "Blog");
            }
            else
            {
                return View(addCategoryForm);
            }
        }

        public ActionResult EditCategory(int categoryId)
        {
            var category = blogRepository.GetCategoryById(categoryId);

            return View(new EditCategoryForm
            {
                Id = categoryId,
                Name = category.Name,
                Description = category.Description
            });
        }

        [HttpPost]
        public ActionResult EditCategory(EditCategoryForm editCategoryForm)
        {
            if (ModelState.IsValid)
            {
                var category = blogRepository.GetCategoryById(editCategoryForm.Id);

                category.Name = editCategoryForm.Name;
                category.Description = editCategoryForm.Description;
                category.UrlSlug = editCategoryForm.Name.Slugify();

                blogRepository.AddOrUpdateCategory(category);

                return RedirectToAction("Posts", "Blog");
            }
            else
            {
                return View(editCategoryForm);
            }
        }

        public ActionResult AddTag()
        {
            return View(new AddTagForm());
        }

        [HttpPost]
        public ActionResult AddTag(AddTagForm addTagForm)
        {
            if (ModelState.IsValid)
            {
                var tag = new Tag()
                {
                    Name = addTagForm.Name,
                    Description = addTagForm.Description,
                    UrlSlug = addTagForm.Name.Slugify()
                };

                blogRepository.AddTag(tag);

                return RedirectToAction("Posts", "Blog");
            }
            else
            {
                return View(addTagForm);
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
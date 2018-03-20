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
using Blog.WebApp.ViewModels.Forms;

namespace Blog.WebApp.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ILoginProvider loginProvider;
        private readonly IImageProvider imageProvider;
        private readonly IPostRepository postRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly ITagRepository tagRepository;
        private readonly IViewModelFactory viewModelFactory;

        public AdminController(ILoginProvider loginProvider, IImageProvider imageProvider,
            IPostRepository postRepository, ICategoryRepository categoryRepository, ITagRepository tagRepository,
            IViewModelFactory viewModelFactory)
        {
            this.loginProvider = loginProvider;
            this.imageProvider = imageProvider;
            this.postRepository = postRepository;
            this.categoryRepository = categoryRepository;
            this.tagRepository = tagRepository;
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

        public ActionResult Images()
        {
            return View(viewModelFactory.GetImages());
        }

        [HttpPost]
        public ActionResult AddImage(HttpPostedFileBase image)
        {
            if (image != null)
            {
                imageProvider.SavePostedImage(image);
            }

            return RedirectToAction("Images");
        }

        public ActionResult AddPost()
        {
            return View(new AddPostForm
            {
                Categories = categoryRepository.GetAll().ToList(),
                Tags = tagRepository.GetAll().ToList()
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
                    Category = categoryRepository.GetById(addPostForm.CategoryId),
                    Tags = tagRepository.GetAll()
                        .Where(tag => addPostForm.TagIds.Contains(tag.Id)).ToList(),
                    ShortDescription = addPostForm.ShortDescription,
                    PostedOn = DateTime.Now,
                    UrlSlug = addPostForm.Title.Slugify()
                };

                postRepository.AddOrUpdate(post);

                return RedirectToAction("Posts", "Blog");
            }
            else
            {
                addPostForm.Categories = categoryRepository.GetAll().ToList();
                addPostForm.Tags = tagRepository.GetAll().ToList();

                return View(addPostForm);
            }
        }

        public ActionResult EditPost(int postId)
        {
            var post = postRepository.GetById(postId);

            return View(new EditPostForm
            {
                Id = postId,
                Title = post.Title,
                Content = post.Content,
                CategoryId = post.Category.Id,
                TagIds = post.Tags.Select(tag => tag.Id).ToList(),
                ShortDescription = post.ShortDescription,

                Categories = categoryRepository.GetAll().ToList(),
                Tags = tagRepository.GetAll().ToList()
            });
        }

        [HttpPost]
        public ActionResult EditPost(EditPostForm editPostForm)
        {
            editPostForm.TagIds = editPostForm.TagIds ?? new List<int>();

            if (ModelState.IsValid)
            {
                var post = postRepository.GetById(editPostForm.Id);

                post.Title = editPostForm.Title;
                post.Content = editPostForm.Content;
                post.Category = categoryRepository.GetById(editPostForm.CategoryId);
                post.Tags = tagRepository.GetAll()
                    .Where(tag => editPostForm.TagIds.Contains(tag.Id)).ToList();
                post.ShortDescription = editPostForm.ShortDescription;
                post.Modified = DateTime.Now;
                post.UrlSlug = editPostForm.Title.Slugify();

                postRepository.AddOrUpdate(post);

                return RedirectToAction("Posts", "Blog");
            }
            else
            {
                editPostForm.Categories = categoryRepository.GetAll().ToList();
                editPostForm.Tags = tagRepository.GetAll().ToList();

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

                categoryRepository.AddOrUpdate(category);

                return RedirectToAction("Posts", "Blog");
            }
            else
            {
                return View(addCategoryForm);
            }
        }

        public ActionResult EditCategory(int categoryId)
        {
            var category = categoryRepository.GetById(categoryId);

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
                var category = categoryRepository.GetById(editCategoryForm.Id);

                category.Name = editCategoryForm.Name;
                category.Description = editCategoryForm.Description;
                category.UrlSlug = editCategoryForm.Name.Slugify();

                categoryRepository.AddOrUpdate(category);

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
                    UrlSlug = addTagForm.Name.Slugify()
                };

                tagRepository.AddOrUpdate(tag);

                return RedirectToAction("Posts", "Blog");
            }
            else
            {
                return View(addTagForm);
            }
        }

        public ActionResult EditTag(int tagId)
        {
            var tag = tagRepository.GetById(tagId);

            return View(new EditTagForm
            {
                Id = tagId,
                Name = tag.Name
            });
        }

        [HttpPost]
        public ActionResult EditTag(EditTagForm editTagForm)
        {
            if (ModelState.IsValid)
            {
                var tag = tagRepository.GetById(editTagForm.Id);

                tag.Name = editTagForm.Name;
                tag.UrlSlug = editTagForm.Name.Slugify();

                tagRepository.AddOrUpdate(tag);

                return RedirectToAction("Posts", "Blog");
            }
            else
            {
                return View(editTagForm);
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
                return RedirectToAction("Posts", "Blog");
            }
        }
    }
}
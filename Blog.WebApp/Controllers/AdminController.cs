using AutoMapper;
using Blog.Core.Models;
using Blog.Core.Repositories;
using Blog.Core.Utility;
using Blog.WebApp.Providers;
using Blog.WebApp.ViewModels;
using Blog.WebApp.ViewModels.Forms;
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

        [HttpGet]
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

                ModelState.AddModelError("", "Podana nazwa użytkownika lub hasło są nieprawidłowe.");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            loginProvider.Logout();

            return RedirectToAction("Posts", "Blog");
        }

        [HttpGet]
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

        [HttpGet]
        public ActionResult DeleteImage(int imageId)
        {
            imageProvider.Delete(imageId);

            return RedirectToAction("Images");
        }

        [HttpGet]
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
                var post = Mapper.Map<Post>(addPostForm);

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

        [HttpGet]
        public ActionResult EditPost(int postId)
        {
            var post = postRepository.GetById(postId);

            var editPostForm = Mapper.Map<EditPostForm>(post);

            editPostForm.Categories = categoryRepository.GetAll().ToList();
            editPostForm.Tags = tagRepository.GetAll().ToList();

            return View(editPostForm);
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
                post.Description = editPostForm.Description;
                post.ModifiedOn = DateTime.Now;
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

        [HttpGet]
        public ActionResult DeletePost(int postId)
        {
            postRepository.Delete(postId);

            return RedirectToAction("Posts", "Blog");
        }

        [HttpGet]
        public ActionResult AddCategory()
        {
            return View(new AddCategoryForm());
        }

        [HttpPost]
        public ActionResult AddCategory(AddCategoryForm addCategoryForm)
        {
            if (ModelState.IsValid)
            {
                var category = Mapper.Map<Category>(addCategoryForm);

                categoryRepository.AddOrUpdate(category);

                return RedirectToAction("Posts", "Blog");
            }
            else
            {
                return View(addCategoryForm);
            }
        }

        [HttpGet]
        public ActionResult EditCategory(int categoryId)
        {
            var category = categoryRepository.GetById(categoryId);

            return View(Mapper.Map<EditCategoryForm>(category));
        }

        [HttpPost]
        public ActionResult EditCategory(EditCategoryForm editCategoryForm)
        {
            if (ModelState.IsValid)
            {
                var category = Mapper.Map<Category>(editCategoryForm);

                categoryRepository.AddOrUpdate(category);

                return RedirectToAction("Posts", "Blog");
            }
            else
            {
                return View(editCategoryForm);
            }
        }

        [HttpGet]
        public ActionResult DeleteCategory(int categoryId)
        {
            categoryRepository.Delete(categoryId);

            return RedirectToAction("Posts", "Blog");
        }

        [HttpGet]
        public ActionResult AddTag()
        {
            return View(new AddTagForm());
        }

        [HttpPost]
        public ActionResult AddTag(AddTagForm addTagForm)
        {
            if (ModelState.IsValid)
            {
                var tag = Mapper.Map<Tag>(addTagForm);

                tagRepository.AddOrUpdate(tag);

                return RedirectToAction("Posts", "Blog");
            }
            else
            {
                return View(addTagForm);
            }
        }

        [HttpGet]
        public ActionResult EditTag(int tagId)
        {
            var tag = tagRepository.GetById(tagId);

            return View(Mapper.Map<EditTagForm>(tag));
        }

        [HttpPost]
        public ActionResult EditTag(EditTagForm editTagForm)
        {
            if (ModelState.IsValid)
            {
                var tag = Mapper.Map<Tag>(editTagForm);

                tagRepository.AddOrUpdate(tag);

                return RedirectToAction("Posts", "Blog");
            }
            else
            {
                return View(editTagForm);
            }
        }

        [HttpGet]
        public ActionResult DeleteTag(int tagId)
        {
            tagRepository.Delete(tagId);

            return RedirectToAction("Posts", "Blog");
        }

        [HttpGet]
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
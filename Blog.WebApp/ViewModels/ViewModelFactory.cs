using Blog.Core.Models;
using Blog.Core.Repositories;
using Blog.WebApp.Properties;
using Blog.WebApp.Providers;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.WebApp.ViewModels
{
    public class ViewModelFactory : IViewModelFactory
    {
        private const int DefaultPageSize = 3;
        private readonly IPostRepository postRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly ITagRepository tagRepository;
        private readonly IImageProvider imageProvider;

        public ViewModelFactory(IPostRepository postRepository,
            ICategoryRepository categoryRepository,
            ITagRepository tagRepository,
            IImageProvider imageProvider)
        {
            this.postRepository = postRepository;
            this.categoryRepository = categoryRepository;
            this.tagRepository = tagRepository;
            this.imageProvider = imageProvider;
        }

        public PostsViewModel GetPosts(int pageNumber)
        {
            var totalPosts = postRepository.TotalPosts();

            return new PostsViewModel
            {
                Posts = postRepository.Posts(pageNumber, DefaultPageSize),
                PagerViewModel = new PagerViewModel(totalPosts, pageNumber, DefaultPageSize),
                Title = string.Empty
            };
        }

        public PostsViewModel GetPostsForCategory(string categorySlug, int pageNumber)
        {
            var category = categoryRepository.GetBySlug(categorySlug);

            if (category == null)
                throw new HttpException(404, Resources.CategoryNotFound);

            var totalPosts = postRepository.TotalPostsForCategory(categorySlug);

            return new PostsViewModel
            {
                Posts = postRepository.PostsForCategory(categorySlug, pageNumber, DefaultPageSize),
                PagerViewModel = new PagerViewModel(totalPosts, pageNumber, DefaultPageSize),
                Title = string.Format(Resources.LatestPostsForCategory, category.Name)
            };
        }

        public PostsViewModel GetPostsForTag(string tagSlug, int pageNumber)
        {
            var tag = tagRepository.GetBySlug(tagSlug);

            if (tag == null)
                throw new HttpException(404, Resources.TagNotFound);

            var totalPosts = postRepository.TotalPostsForTag(tagSlug);

            return new PostsViewModel
            {
                Posts = postRepository.PostsForTag(tagSlug, pageNumber, DefaultPageSize),
                PagerViewModel = new PagerViewModel(totalPosts, pageNumber, DefaultPageSize),
                Title = string.Format(Resources.LatestPostsForTag, tag.Name)
            };
        }

        public PostsViewModel GetPostsBySearch(string searchPhrase, int pageNumber)
        {
            var totalPosts = postRepository.TotalPostsBySearch(searchPhrase);

            return new PostsViewModel
            {
                Posts = postRepository.PostsBySearch(searchPhrase, pageNumber, DefaultPageSize),
                PagerViewModel = new PagerViewModel(totalPosts, pageNumber, DefaultPageSize),
                Title = string.Format(Resources.SearchResultForPhrase, searchPhrase)
            };
        }

        public Post GetPostDetails(int year, int month, string postSlug)
        {
            return postRepository.PostDetails(year, month, postSlug);
        }

        public LeftSidebarViewModel GetLeftSidebar()
        {
            return new LeftSidebarViewModel
            {
                Categories = categoryRepository.GetAll().ToList(),
                LatestPosts = postRepository.Posts(pageNumber: 1, pageSize: 8),
                Tags = tagRepository.GetAll().ToList()
            };
        }

        public IList<ImageViewModel> GetImages()
        {
            return imageProvider.GetImages()
                .Select(image => new ImageViewModel
                {
                    Id = image.Id,
                    ImageUrl = imageProvider.GetImageUrl(image),
                    ThumbnailUrl = imageProvider.GetThumbnailUrl(image)
                }).ToList();
        }
    }
}
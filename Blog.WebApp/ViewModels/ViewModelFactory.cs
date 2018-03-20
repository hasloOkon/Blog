﻿using Blog.Core.Models;
using Blog.Core.Repositories;
using Blog.WebApp.Providers;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.WebApp.ViewModels
{
    public class ViewModelFactory : IViewModelFactory
    {
        private const int PageSize = 3;
        private readonly IBlogRepository blogRepository;
        private readonly IImageRepository imageRepository;
        private readonly IImageProvider imageProvider;

        public ViewModelFactory(IBlogRepository blogRepository, IImageRepository imageRepository,
            IImageProvider imageProvider)
        {
            this.blogRepository = blogRepository;
            this.imageRepository = imageRepository;
            this.imageProvider = imageProvider;
        }

        public PostsViewModel GetPosts(int pageNumber)
        {
            var totalPosts = blogRepository.TotalPosts();

            return new PostsViewModel
            {
                Posts = blogRepository.Posts(pageNumber, PageSize),
                PagerViewModel = new PagerViewModel(totalPosts, pageNumber, PageSize),
                Title = string.Empty
            };
        }

        public PostsViewModel GetPostsForCategory(string categorySlug, int pageNumber)
        {
            var category = blogRepository.Category(categorySlug);

            if (category == null)
                throw new HttpException(404, "Nie znaleziono kategorii :(");

            var totalPosts = blogRepository.TotalPostsForCategory(categorySlug);

            return new PostsViewModel
            {
                Posts = blogRepository.PostsForCategory(categorySlug, pageNumber, PageSize),
                PagerViewModel = new PagerViewModel(totalPosts, pageNumber, PageSize),
                Title = $"Ostatnie posty dla kategorii \"{category.Name}\":"
            };
        }

        public PostsViewModel GetPostsForTag(string tagSlug, int pageNumber)
        {
            var tag = blogRepository.Tag(tagSlug);

            if (tag == null)
                throw new HttpException(404, "Nie znaleziono taga :(");

            var totalPosts = blogRepository.TotalPostsForTag(tagSlug);

            return new PostsViewModel
            {
                Posts = blogRepository.PostsForTag(tagSlug, pageNumber, PageSize),
                PagerViewModel = new PagerViewModel(totalPosts, pageNumber, PageSize),
                Title = $"Ostatnie posty dla taga \"{tag.Name}\":"
            };
        }

        public PostsViewModel GetPostsBySearch(string searchPhrase, int pageNumber)
        {
            var totalPosts = blogRepository.TotalPostsBySearch(searchPhrase);

            return new PostsViewModel
            {
                Posts = blogRepository.PostsBySearch(searchPhrase, pageNumber, PageSize),
                PagerViewModel = new PagerViewModel(totalPosts, pageNumber, PageSize),
                Title = $"Search results for phrase \"{searchPhrase}\""
            };
        }

        public Post GetPostDetails(int year, int month, string postSlug)
        {
            return blogRepository.PostDetails(year, month, postSlug);
        }

        public LeftSidebarViewModel GetLeftSidebar()
        {
            return new LeftSidebarViewModel
            {
                Categories = blogRepository.Categories(),
                LatestPosts = blogRepository.Posts(pageNumber: 1, pageSize: PageSize),
                Tags = blogRepository.Tags()
            };
        }

        public IList<ImageViewModel> GetImages()
        {
            var images = imageRepository.Images().ToList();

            var imageViewModels = images
                .Select(image => new ImageViewModel
                {
                    Id = image.Id,
                    ImagePath = imageProvider.GetImageUrl(image),
                    ThumbnailPath = imageProvider.GetThumbnailUrl(image)
                }).ToList();

            return imageViewModels;
        }
    }
}
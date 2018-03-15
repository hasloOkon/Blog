using System.Web;
using Blog.Core;
using Blog.Core.Models;

namespace Blog.WebApp.ViewModels
{
    public class ViewModelFactory : IViewModelFactory
    {
        private const int PageSize = 5;
        private readonly IBlogRepository blogRepository;

        public ViewModelFactory(IBlogRepository blogRepository)
        {
            this.blogRepository = blogRepository;
        }

        public PostsViewModel GetPosts(int pageNumber)
        {
            return new PostsViewModel
            {
                Posts = blogRepository.Posts(pageNumber, PageSize),
                TotalPosts = blogRepository.TotalPosts(),
                Title = string.Empty
            };
        }

        public PostsViewModel GetPostsForCategory(string categorySlug, int pageNumber)
        {
            var category = blogRepository.Category(categorySlug);

            if (category == null)
                throw new HttpException(404, "Category not found");

            return new PostsViewModel
            {
                Posts = blogRepository.PostsForCategory(categorySlug, pageNumber, PageSize),
                TotalPosts = blogRepository.TotalPostsForCategory(categorySlug),
                Title = $"Latest Posts for category \"{category.Name}\""
            };
        }   

        public PostsViewModel GetPostsForTag(string tagSlug, int pageNumber)
        {
            var tag = blogRepository.Tag(tagSlug);

            if (tag == null)
                throw new HttpException(404, "Tag not found");

            return new PostsViewModel
            {
                Posts = blogRepository.PostsForTag(tagSlug, pageNumber, PageSize),
                TotalPosts = blogRepository.TotalPostsForTag(tagSlug),
                Title = $"Lastest Posts for tag \"{tag.Name}\""
            };
        }

        public PostsViewModel GetPostsBySearch(string searchPhrase, int pageNumber)
        {
            return new PostsViewModel
            {
                Posts = blogRepository.PostsBySearch(searchPhrase, pageNumber, PageSize),
                TotalPosts = blogRepository.TotalPostsBySearch(searchPhrase),
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
                LatestPosts = blogRepository.Posts(pageNumber: 1, pageSize: PageSize)
            };
        }

        public RightSidebarViewModel GetRightSidebar()
        {
            return new RightSidebarViewModel
            {
                Tags = blogRepository.Tags()
            };
        }
    }
}
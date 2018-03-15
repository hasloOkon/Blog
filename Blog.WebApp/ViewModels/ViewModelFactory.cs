using System.Web;
using Blog.Core;
using Blog.Core.Models;

namespace Blog.WebApp.ViewModels
{
    public class ViewModelFactory : IViewModelFactory
    {
        private const int PageSize = 3;
        private readonly IBlogRepository blogRepository;

        public ViewModelFactory(IBlogRepository blogRepository)
        {
            this.blogRepository = blogRepository;
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
                throw new HttpException(404, "Category not found");

            var totalPosts = blogRepository.TotalPostsForCategory(categorySlug);

            return new PostsViewModel
            {
                Posts = blogRepository.PostsForCategory(categorySlug, pageNumber, PageSize),
                PagerViewModel = new PagerViewModel(totalPosts, pageNumber, PageSize),
                Title = $"Latest Posts for category \"{category.Name}\""
            };
        }   

        public PostsViewModel GetPostsForTag(string tagSlug, int pageNumber)
        {
            var tag = blogRepository.Tag(tagSlug);

            if (tag == null)
                throw new HttpException(404, "Tag not found");

            var totalPosts = blogRepository.TotalPostsForTag(tagSlug);

            return new PostsViewModel
            {
                Posts = blogRepository.PostsForTag(tagSlug, pageNumber, PageSize),
                PagerViewModel = new PagerViewModel(totalPosts, pageNumber, PageSize),
                Title = $"Lastest Posts for tag \"{tag.Name}\""
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
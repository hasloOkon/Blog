using Blog.Core.Models;
using System.Collections.Generic;

namespace Blog.WebApp.ViewModels
{
    public interface IViewModelFactory
    {
        PostsViewModel GetPosts(int pageNumber);
        PostsViewModel GetPostsForCategory(string categorySlug, int pageNumber);
        PostsViewModel GetPostsForTag(string tagSlug, int pageNumber);
        PostsViewModel GetPostsBySearch(string searchPhrase, int pageNumber);
        Post GetPostDetails(int year, int month, string postSlug);
        LeftSidebarViewModel GetLeftSidebar();
        IList<ImageViewModel> GetImages();
    }
}

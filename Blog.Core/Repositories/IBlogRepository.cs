using Blog.Core.Models;
using System.Collections.Generic;

namespace Blog.Core.Repositories
{
    public interface IBlogRepository
    {
        IList<Post> Posts(int pageNumber, int pageSize);
        int TotalPosts();

        IList<Post> PostsForCategory(string categorySlug, int pageNumber, int pageSize);
        int TotalPostsForCategory(string categorySlug);
        Category Category(string categorySlug);

        IList<Post> PostsForTag(string tagSlug, int pageNumber, int pageSize);
        int TotalPostsForTag(string tagSlug);
        Tag Tag(string tagSlug);

        IList<Post> PostsBySearch(string searchPhrase, int pageNumber, int pageSize);
        int TotalPostsBySearch(string searchPhrase);

        Post PostDetails(int year, int month, string postSlug);

        IList<Category> Categories();
        IList<Tag> Tags();

        void AddOrUpdatePost(Post post);

        void AddCategory(Category category);

        void AddTag(Tag tag);

        Post GetPostById(int id);
    }
}

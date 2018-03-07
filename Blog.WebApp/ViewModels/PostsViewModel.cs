using System.Collections.Generic;
using Blog.Core.Models;

namespace Blog.WebApp.ViewModels
{
    public class PostsViewModel
    {
        public IList<Post> Posts { get; set; }
        public int TotalPosts { get; set; }
        public string Title { get; set; }
    }
}

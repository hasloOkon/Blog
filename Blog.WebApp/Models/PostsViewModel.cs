using System.Collections.Generic;
using Blog.Core.DomainObjects;

namespace Blog.WebApp.Models
{
    public class PostsViewModel
    {
        public IList<Post> Posts { get; set; }
        public int TotalPosts { get; set; }
    }
}
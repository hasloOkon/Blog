using System.Collections.Generic;
using Blog.Core.Models;

namespace Blog.WebApp.ViewModels
{
    public class PostsViewModel
    {
        public IList<Post> Posts { get; set; }
        public PagerViewModel PagerViewModel { get; set; }
        public string Title { get; set; }
    }
}

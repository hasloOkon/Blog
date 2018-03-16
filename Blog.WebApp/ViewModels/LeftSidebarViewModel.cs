using System.Collections.Generic;
using Blog.Core.Models;

namespace Blog.WebApp.ViewModels
{
    public class LeftSidebarViewModel
    {
        public IList<Category> Categories { get; set; }
        public IList<Post> LatestPosts { get; set; }
        public IList<Tag> Tags { get; set; }
    }
}
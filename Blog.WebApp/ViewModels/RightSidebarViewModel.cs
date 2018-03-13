using System.Collections.Generic;
using Blog.Core.Models;

namespace Blog.WebApp.ViewModels
{
    public class RightSidebarViewModel
    {
        public IList<Tag> Tags { get; set; }
    }
}
using System.Collections.Generic;
using Blog.Core.DomainObjects;

namespace Blog.WebApp.Models
{
    public class SidebarViewModel
    {
        public IList<Category> Categories { get; set; }
    }
}
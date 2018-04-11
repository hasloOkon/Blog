using Blog.Core.Models;
using Blog.WebApp.Properties;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Blog.WebApp.ViewModels.Forms
{
    public class EditPostForm
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "PostTitle", ResourceType = typeof(Resources))]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        [StringLength(255)]
        [Display(Name = "PostDescription", ResourceType = typeof(Resources))]
        public string Description { get; set; }

        [Required]
        [AllowHtml]
        [Display(Name = "PostContent", ResourceType = typeof(Resources))]
        public string Content { get; set; }

        [Display(Name = "PostCategory", ResourceType = typeof(Resources))]
        public int CategoryId { get; set; }

        [Display(Name = "PostTags", ResourceType = typeof(Resources))]
        public IList<int> TagIds { get; set; }

        public IList<Category> Categories { get; set; }

        public IList<Tag> Tags { get; set; }
    }
}
using Blog.WebApp.Properties;
using System.ComponentModel.DataAnnotations;

namespace Blog.WebApp.ViewModels.Forms
{
    public class AddTagForm
    {
        [Required]
        [StringLength(100)]
        [Display(Name = "TagName", ResourceType = typeof(Resources))]
        public string Name { get; set; }
    }
}
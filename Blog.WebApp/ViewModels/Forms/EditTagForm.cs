using Blog.WebApp.Properties;
using System.ComponentModel.DataAnnotations;

namespace Blog.WebApp.ViewModels.Forms
{
    public class EditTagForm
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "TagName", ResourceType = typeof(Resources))]
        public string Name { get; set; }
    }
}
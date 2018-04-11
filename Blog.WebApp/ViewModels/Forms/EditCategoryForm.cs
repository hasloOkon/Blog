using Blog.WebApp.Properties;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Blog.WebApp.ViewModels.Forms
{
    public class EditCategoryForm
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "CategoryName", ResourceType = typeof(Resources))]
        public string Name { get; set; }

        [AllowHtml]
        [StringLength(255)]
        [Display(Name = "CategoryDescription", ResourceType = typeof(Resources))]
        public string Description { get; set; }
    }
}
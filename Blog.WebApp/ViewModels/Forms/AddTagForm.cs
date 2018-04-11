using System.ComponentModel.DataAnnotations;

namespace Blog.WebApp.ViewModels.Forms
{
    public class AddTagForm
    {
        [Required]
        [StringLength(100)]
        [Display(Name = "Nazwa taga")]
        public string Name { get; set; }
    }
}
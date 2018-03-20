using System.ComponentModel.DataAnnotations;

namespace Blog.WebApp.ViewModels.Forms
{
    public class AddTagForm
    {
        [Required]
        [StringLength(500)]
        [Display(Name = "Nazwa taga")]
        public string Name { get; set; }
    }
}
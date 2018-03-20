using System.ComponentModel.DataAnnotations;

namespace Blog.WebApp.ViewModels.Forms
{
    public class EditTagForm
    {
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "Nazwa taga")]
        public string Name { get; set; }
    }
}
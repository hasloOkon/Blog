using System.ComponentModel.DataAnnotations;

namespace Blog.WebApp.ViewModels.Forms
{
    public class EditTagForm
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Nazwa taga")]
        public string Name { get; set; }
    }
}
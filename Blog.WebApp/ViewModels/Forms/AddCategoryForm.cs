using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Blog.WebApp.ViewModels.Forms
{
    public class AddCategoryForm
    {
        [Required]
        [StringLength(100)]
        [Display(Name = "Nazwa kategorii")]
        public string Name { get; set; }

        [AllowHtml]
        [StringLength(255)]
        [Display(Name = "Opis kategorii")]
        public string Description { get; set; }
    }
}
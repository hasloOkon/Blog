using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Blog.WebApp.ViewModels
{
    public class AddTagForm
    {
        [Required]
        [StringLength(500)]
        [Display(Name = "Nazwa taga")]
        public string Name
        { get; set; }

        [AllowHtml]
        [Display(Name = "Opis taga")]
        public string Description
        { get; set; }
    }
}
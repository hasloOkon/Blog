using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Blog.WebApp.ViewModels
{
    public class AddPostForm
    {
        [Required]
        [StringLength(500)]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        [Display(Name = "Krótki opis")]
        public string ShortDescription { get; set; }

        [Required]
        [AllowHtml]
        [Display(Name = "Treść")]
        public string Content { get; set; }

        public bool Published { get; set; }

        public int CategoryId { get; set; }

        public IList<int> TagIds { get; set; }
    }
}
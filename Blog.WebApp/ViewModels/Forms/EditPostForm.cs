using Blog.Core.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Blog.WebApp.ViewModels.Forms
{
    public class EditPostForm
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        [StringLength(255)]
        [Display(Name = "Krótki opis")]
        public string Description { get; set; }

        [Required]
        [AllowHtml]
        [Display(Name = "Treść")]
        public string Content { get; set; }

        [Display(Name = "Kategoria")]
        public int CategoryId { get; set; }

        [Display(Name = "Tagi")]
        public IList<int> TagIds { get; set; }

        public IList<Category> Categories { get; set; }

        public IList<Tag> Tags { get; set; }
    }
}
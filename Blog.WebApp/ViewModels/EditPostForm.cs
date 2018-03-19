using Blog.Core.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Blog.WebApp.ViewModels
{
    public class EditPostForm
    {
        public int Id { get; set; }

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

        [Display(Name = "Opublikowany")]
        public bool Published { get; set; }

        [Display(Name = "Kategoria")]
        public int CategoryId { get; set; }

        [Display(Name = "Tagi")]
        public IList<int> TagIds { get; set; }

        public IList<Category> Categories { get; set; }

        public IList<Tag> Tags { get; set; }
    }
}
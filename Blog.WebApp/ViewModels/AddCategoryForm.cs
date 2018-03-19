using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.WebApp.ViewModels
{
    public class AddCategoryForm
    {
        [Required]
        [StringLength(500)]
        [Display(Name = "Nazwa kategorii")]
        public string Name { get; set; }

        [AllowHtml]
        [Display(Name = "Opis kategorii")]
        public string Description { get; set; }
    }
}
﻿using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Blog.WebApp.ViewModels.Forms
{
    public class EditCategoryForm
    {
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "Nazwa kategorii")]
        public string Name { get; set; }

        [AllowHtml]
        [Display(Name = "Opis kategorii")]
        public string Description { get; set; }
    }
}
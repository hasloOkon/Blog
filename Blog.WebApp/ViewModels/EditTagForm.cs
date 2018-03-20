using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.WebApp.ViewModels
{
    public class EditTagForm
    {
        public int Id { get; set; }

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
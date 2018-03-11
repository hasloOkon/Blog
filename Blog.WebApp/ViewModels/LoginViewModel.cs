using System.ComponentModel.DataAnnotations;

namespace Blog.WebApp.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "User name is required")]
        [Display(Name = "User name (*)")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password (*)")]
        public string Password { get; set; }
    }
}
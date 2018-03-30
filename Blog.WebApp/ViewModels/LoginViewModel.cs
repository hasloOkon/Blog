using System.ComponentModel.DataAnnotations;

namespace Blog.WebApp.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Nazwa użytkownika jest wymagana!")]
        [Display(Name = "Nazwa użytkownika")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane!")]
        [Display(Name = "Hasło")]
        public string Password { get; set; }
    }
}
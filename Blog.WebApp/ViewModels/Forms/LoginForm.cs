using Blog.WebApp.Properties;
using System.ComponentModel.DataAnnotations;

namespace Blog.WebApp.ViewModels.Forms
{
    public class LoginForm
    {
        [Required(ErrorMessageResourceName = "UsernameRequired", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Username", ResourceType = typeof(Resources))]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceName = "PasswordRequired", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Password", ResourceType = typeof(Resources))]
        public string Password { get; set; }
    }
}
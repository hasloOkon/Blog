using System.Web;
using System.Web.Security;

namespace Blog.WebApp.Providers
{
    public class LoginProvider : ILoginProvider
    {
        public bool IsLoggedIn => HttpContext.Current.User.Identity.IsAuthenticated;

        public bool Login(string username, string password)
        {
            var authenticationSuccessful = FormsAuthentication.Authenticate(username, password);

            if (authenticationSuccessful)
            {
                FormsAuthentication.SetAuthCookie(username, createPersistentCookie: false);
            }

            return authenticationSuccessful;
        }

        public void Logout()
        {
            FormsAuthentication.SignOut();
        }
    }
}
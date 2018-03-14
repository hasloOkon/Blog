namespace Blog.WebApp.Providers
{
    public interface ILoginProvider
    {
        bool IsLoggedIn { get; }
        bool Login(string username, string password);
        void Logout();
    }
}

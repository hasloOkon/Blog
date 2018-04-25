using System.Web.Mvc;
using HandleErrorAttribute = Blog.WebApp.Utility.HandleErrorAttribute;

namespace Blog.WebApp
{
    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
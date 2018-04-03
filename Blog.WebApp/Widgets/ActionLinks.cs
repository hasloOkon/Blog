using Blog.Core.Models;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Blog.WebApp.Widgets
{
    public static class ActionLinks
    {
        public static MvcHtmlString PostLink(this HtmlHelper helper, Post post, string title)
        {
            return helper.ActionLink(title, "PostDetails", "Blog",
                new
                {
                    year = post.PostedOn.Year,
                    month = post.PostedOn.Month,
                    postSlug = post.UrlSlug
                },
                new
                {
                    title = title
                });
        }

        public static MvcHtmlString CategoryLink(this HtmlHelper helper,
            Category category)
        {
            return helper.ActionLink(category.Name, "Category", "Blog",
                new
                {
                    categorySlug = category.UrlSlug
                },
                new
                {
                    title = $"Posty w kategorii: {category.Name}"
                });
        }

        public static MvcHtmlString TagLink(this HtmlHelper helper, Tag tag)
        {
            return helper.ActionLink(tag.Name, "Tag", "Blog",
                new
                {
                    tagSlug = tag.UrlSlug
                },
                new
                {
                    title = $"Posty dla taga: {tag.Name}",
                    @class = "tag-link"
                });
        }

        public static MvcHtmlString LoginLink(this HtmlHelper helper, string linkText)
        {
            var actionName = HttpContext.Current.User.Identity.IsAuthenticated ? "Logout" : "Login";

            return helper.ActionLink(linkText, actionName, "Admin");
        }

        public static MvcHtmlString ImagesLink(this HtmlHelper helper,
            string linkText,
            bool openInNewTab = false)
        {
            var htmlAttributes = openInNewTab
                ? new { target = "_blank" }
                : null;

            return helper.ActionLink(linkText, "Images", "Admin", null, htmlAttributes);
        }
    }
}
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Blog.Core.Models;

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
                    title = $"See all posts in {category.Name}"
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
                    title = $"See all posts in {tag.Name}"
                });
        }

        public static MvcHtmlString LoginLink(this HtmlHelper helper, string name)
        {
            return helper.ActionLink(name, "Login", "Admin");
        }
    }
}
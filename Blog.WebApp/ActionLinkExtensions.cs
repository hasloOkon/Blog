using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Blog.Core.DomainObjects;

namespace Blog.WebApp
{
    public static class ActionLinkExtensions
    {
        public static MvcHtmlString PostLink(this HtmlHelper helper, Post post)
        {
            return helper.ActionLink(post.Title, "Post", "Blog",
                new
                {
                    year = post.PostedOn.Year,
                    month = post.PostedOn.Month,
                    title = post.UrlSlug
                },
                new
                {
                    title = post.Title
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
    }
}
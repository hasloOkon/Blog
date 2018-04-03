using Blog.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Blog.WebApp.Widgets
{
    public static class Widgets
    {
        public static MvcHtmlString CategoryDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> selector, IEnumerable<Category> categories)
        {
            return html.DropDownListFor(
                selector,
                categories.ToSelectList(category => category.Name, category => category.Id),
                new { @class = "form-control" });
        }

        public static MvcHtmlString TagListBoxFor<TModel, TProperty>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> selector, IEnumerable<Tag> tags)
        {
            return html.ListBoxFor(
                selector,
                tags.ToSelectList(tag => tag.Name, tag => tag.Id),
                new { @class = "form-control" });
        }

        private static IEnumerable<SelectListItem> ToSelectList<T>(this IEnumerable<T> items,
            Func<T, object> textSelector,
            Func<T, object> valueSelector)
        {
            return items.Select(item => new SelectListItem
            {
                Text = textSelector(item).ToString(),
                Value = valueSelector(item).ToString()
            });
        }
    }
}
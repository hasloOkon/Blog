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
            Expression<Func<TModel, TProperty>> selector, IEnumerable<Category> categories,
            IDictionary<string, object> htmlAttributes = null)
        {
            return html.DropDownListFor(
                selector,
                categories.ToSelectList(category => category.Name, category => category.Id),
                htmlAttributes ?? new Dictionary<string, object>());
        }

        public static MvcHtmlString TagListBoxFor<TModel, TProperty>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> selector, IEnumerable<Tag> tags,
            IDictionary<string, object> htmlAttributes = null)
        {
            return html.ListBoxFor(
                selector,
                tags.ToSelectList(tag => tag.Name, tag => tag.Id),
                htmlAttributes ?? new Dictionary<string, object>());
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
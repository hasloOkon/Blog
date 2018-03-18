using System.Collections.Generic;
using System.Web.Mvc;

namespace Blog.WebApp.Widgets
{
    public static class HtmlHelperExtensions
    {
        public static HtmlAttributesFluent Attributes(this HtmlHelper html)
        {
            return new HtmlAttributesFluent();
        }
    }

    public class HtmlAttributesFluent : Dictionary<string, object>
    {
        public HtmlAttributesFluent Id(string id)
        {
            this["id"] = id;

            return this;
        }

        public HtmlAttributesFluent Class(params string[] classes)
        {
            this["class"] = string.Join(" ", classes);

            return this;
        }
    }
}
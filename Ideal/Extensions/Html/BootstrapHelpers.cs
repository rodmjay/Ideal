using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Ideal.Html
{
    public static class BootstrapHelpers
    {
        private static RouteValueDictionary MergeDictionary(RouteValueDictionary input)
        {
            if (null==input) input = new RouteValueDictionary();
            RouteValueDictionary dictionary = new RouteValueDictionary(input){
                ["class"] = "form-control"
            };
            return dictionary;
        }

        public static MvcHtmlString BootstrapTextBox(this HtmlHelper helper, string name)
        {
            return helper.BootstrapTextBox(name, null);
        }

        public static MvcHtmlString BootstrapTextBox(this HtmlHelper helper, string name, object formattedModelValue)
        {
            return helper.BootstrapTextBox(name, formattedModelValue, null);
        }

        public static MvcHtmlString BootstrapTextBox(this HtmlHelper helper, string name, object formattedModelValue, object htmlAttributes)
        {
            RouteValueDictionary merged = MergeDictionary(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            return helper.TextBox(name, formattedModelValue, merged);
        }

        public static IDictionary<string, object> GetBootstrapAttributes(this HtmlHelper helper)
        {
            ViewDataDictionary viewData = new ViewDataDictionary(helper.ViewData);
            return MergeDictionary(new RouteValueDictionary());
        }
    }
}
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNet.Mvc.Rendering;

namespace Microsoft.AspNet.Mvc.Rendering
{
    public static partial class PantherHelper
    {
        //public static HtmlString RenderRegisteredStylesheets<TModel>(this IHtmlHelper<TModel> htmlHelper)
        //{
        //    var ctx = htmlHelper.ViewContext.HttpContext;
        //    var registeredStylesheets = ctx.Items["_stylesheets_"] as Stack<string>;
        //    if (registeredStylesheets == null || registeredStylesheets.Count < 1)
        //    {
        //        return null;
        //    }
        //    var sb = new StringBuilder();
        //    foreach (var script in registeredStylesheets)
        //    {
        //        var scriptBuilder = new TagBuilder("link");
        //        scriptBuilder.Attributes["rel"] = "stylesheet";
        //        scriptBuilder.Attributes["href"] = script;
        //        sb.AppendLine(scriptBuilder.ToString(TagRenderMode.StartTag));
        //    }
        //    return new HtmlString(sb.ToString());
        //}

        public static HtmlString RenderRegisteredStylesheets(this IHtmlHelper htmlHelper)
        {
            var ctx = htmlHelper.ViewContext.HttpContext;
            var registeredStylesheets = ctx.Items["_stylesheets_"] as Stack<string>;
            if (registeredStylesheets == null || registeredStylesheets.Count < 1)
            {
                return null;
            }
            var sb = new StringBuilder();
            foreach (var script in registeredStylesheets)
            {
                var scriptBuilder = new TagBuilder("link");
                scriptBuilder.Attributes["rel"] = "stylesheet";
                scriptBuilder.Attributes["href"] = script;
                sb.AppendLine(scriptBuilder.ToString(TagRenderMode.StartTag));
            }
            return new HtmlString(sb.ToString());
        }

        public static void RegisterStylesheet(this IHtmlHelper htmlHelper, string stylesheet)
        {
            var ctx = htmlHelper.ViewContext.HttpContext;

            //var urlHelper = new UrlHelper(htmlHelper.ViewContext.HttpContext.Request);// new UrlHelper();
            var registeredStylesheets = ctx.Items["_stylesheets_"] as Stack<string>;
            if (registeredStylesheets == null)
            {
                registeredStylesheets = new Stack<string>();
                ctx.Items["_stylesheets_"] = registeredStylesheets;
            }
            var src = stylesheet;
            if (!registeredStylesheets.Contains(src))
            {
                registeredStylesheets.Push(src);
            }
        }
    }
}
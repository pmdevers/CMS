using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Microsoft.AspNet.Html.Abstractions;
using Microsoft.AspNet.Mvc.Rendering;

using Panther.CMS.Helpers;

namespace Microsoft.AspNet.Mvc.Rendering
{
    public static partial class PantherHelper
    {
        public static IHtmlContent StyleSheets(this IHtmlHelper htmlHelper)
        {
            var stylesheets = htmlHelper.Panther().Site.Stylesheets;
            stylesheets.ForEach(htmlHelper.RegisterStylesheet);

            var sb = new StringBuilder();
            sb.Append(htmlHelper.RenderRegisteredStylesheets());
            
            var css = DelayedBlock.GetBlock(htmlHelper, "style");
            var minifiedCss = Minifier.RemoveWhiteSpaceFromStylesheets(css);

            if (!string.IsNullOrWhiteSpace(minifiedCss))
            {
                sb.Append("<style type=\"text/css\">");
                sb.Append(minifiedCss);
                sb.Append("</style>");
            }
            return new HtmlString(sb.ToString());
        }

        public static IHtmlContent RenderRegisteredStylesheets(this IHtmlHelper htmlHelper)
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
                //var scriptBuilder = new TagBuilder("link");
                //scriptBuilder.Attributes["rel"] = "stylesheet";
                //scriptBuilder.Attributes["href"] = script;
                //scriptBuilder.TagRenderMode = TagRenderMode.StartTag;
                //sb.AppendLine(scriptBuilder.ToString());

                sb.AppendLine($"<link rel=\"stylesheet\" href=\"{script}\" >");
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

        public static IDisposable RegisterStylesheet(this IHtmlHelper htmlHelper)
        {
             return new DelayedBlock(htmlHelper, "style");
        }

    }
}
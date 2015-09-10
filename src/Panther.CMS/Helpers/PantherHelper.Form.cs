using System.Collections.Generic;

using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.WebEncoders;

using Panther.CMS.Entities;
using Panther.CMS.Interfaces;

namespace Microsoft.AspNet.Mvc.Rendering

{
    public static partial class PantherHelper
    {
        public static MvcForm PantherForm(this IHtmlHelper htmlHelper, FormMethod method = FormMethod.Post, IDictionary<string, object> htmlAttributes = null)
        {
            var context = htmlHelper.ViewContext.HttpContext.ApplicationServices.GetService<IPantherContext>();
            return htmlHelper.PantherForm(context.Path, method, htmlAttributes);
        }

        public static MvcForm PantherForm(this IHtmlHelper htmlHelper, Page page, FormMethod method = FormMethod.Post, IDictionary<string, object> htmlAttributes = null)
        {
            return htmlHelper.PantherForm(page.Path, method, htmlAttributes);
        }

        public static MvcForm PantherForm(this IHtmlHelper htmlHelper, string Path, FormMethod method = FormMethod.Post, IDictionary<string, object> htmlAttributes = null)
        {
            var htmlEncoder = htmlHelper.ViewContext.HttpContext.ApplicationServices.GetService<IHtmlEncoder>();
            TagBuilder builder = new TagBuilder("form");
            builder.MergeAttributes(htmlAttributes);
            builder.MergeAttribute("action", Path);
            builder.MergeAttribute("method", HtmlHelper.GetFormMethodString(method), true);
            builder.TagRenderMode = TagRenderMode.StartTag;


            builder.WriteTo(htmlHelper.ViewContext.Writer, htmlEncoder);

            return new MvcForm(htmlHelper.ViewContext);
        }
    }
}
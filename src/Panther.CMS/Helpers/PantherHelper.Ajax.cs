using Microsoft.AspNet.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.AspNet.Html.Abstractions;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.AspNet.Mvc.Rendering
{
    public static partial class PantherHelper
    {
        public static MvcForm AjaxForm(this IHtmlHelper htmlHelper, AjaxOptions options)
        {
            var builder = new TagBuilder("form");
            if(!string.IsNullOrEmpty(options.Url))
                builder.MergeAttribute("data-ajax-url", options.Url);

            builder.MergeAttribute("data-ajax-method", HtmlHelper.GetFormMethodString(options.FormMethod), true);
            builder.MergeAttribute("data-ajax", "true");

            var mode = PantherHelper.GetInsertionModeString(options.InsertionMode);
            if (!string.IsNullOrEmpty(mode))
                builder.MergeAttribute("data-ajax-mode", mode);
            if (!string.IsNullOrEmpty(options.JQuerySelector))
                builder.MergeAttribute("data-ajax-update", options.JQuerySelector);

            htmlHelper.ViewContext.Writer.Write(builder.ToString(TagRenderMode.StartTag));

            return new MvcForm(htmlHelper.ViewContext);
        }

        //public static Task<HtmlString> AjaxPartial(this HtmlHelper htmlHelper, string url)
        //{
        //    //using (var client = new HttpClient())
        //    //{
        //    //    var uri = new Uri(url);
        //    //    var result = client.GetStringAsync(uri).Result;
        //    //    return Task.FromResult(new HtmlString(result));
        //    //}
        //}

       

        public static string GetInsertionModeString(InsertionMode mode)
        {
            switch (mode)
            {
                case InsertionMode.InsertAfter:
                    return "after";
                case InsertionMode.InsertBefore:
                    return "before";
                case InsertionMode.ReplaceWith:
                    return "replace-with";
                default:
                    return string.Empty;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Framework.DependencyInjection;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;

using Panther.CMS.Entities;
using Panther.CMS.Interfaces;
using Panther.CMS.Services.Page;

namespace Microsoft.AspNet.Mvc.Rendering
{
    public static partial class PantherHelper
    {
        public static HtmlString Canonicals(this IHtmlHelper htmlHelper)
        {
            var context = htmlHelper.Panther();
            var page = context.Current;

            var sb = new StringBuilder();

            sb.AppendLine(Canonical(htmlHelper, page, context.Site).ToString());
            foreach (var pageId in page.Canonicals)
            {
                sb.AppendLine(Canonical(htmlHelper, pageId).ToString());
            }

            return new HtmlString(sb.ToString());
        }

        public static HtmlString Canonical(this IHtmlHelper htmlHelper, Guid pageId)
        {
            var pageService = htmlHelper.ViewContext.HttpContext.ApplicationServices.GetService<IPageService>();
            var context = htmlHelper.Panther();
            var page = pageService.Get(pageId);
            return Canonical(htmlHelper, page, context.Site);
        }

        public static HtmlString Canonical(this IHtmlHelper htmlHelper, Page page, Site site)
        {
            var builder = new TagBuilder("link");
            //<link rel="alternate" href="http://example.com/en-ie" hreflang="en-ie" />
            builder.Attributes.Add("rel", "alternate");
            builder.Attributes.Add("href", ("http://" + site.Url + page.Path).TrimEnd('/'));
            builder.Attributes.Add("hreflang", page.Culture.ToLowerInvariant());

            return builder.ToHtmlString(TagRenderMode.SelfClosing);
        }

    }
}

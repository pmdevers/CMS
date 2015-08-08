using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Framework.DependencyInjection;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Panther.CMS.Entities;
using Panther.CMS.Interfaces;
using Panther.CMS.Services.Page;
using Panther.CMS.Storage.Resource;

namespace Microsoft.AspNet.Mvc.Rendering
{
    public static partial class PantherHelper
    {
        public static HtmlString Translations(this IHtmlHelper htmlHelper)
        {
            var context = htmlHelper.Panther();
            var page = context.Current;

            var sb = new StringBuilder();

            sb.AppendLine(Translation(htmlHelper, page, context.Site).ToString());
            foreach (var pageId in page.Translations)
            {
                sb.AppendLine(Translation(htmlHelper, pageId).ToString());
            }

            return new HtmlString(sb.ToString());
        }

        public static HtmlString Translation(this IHtmlHelper htmlHelper, Guid pageId)
        {
            var context = htmlHelper.Panther();
            var page = context.Root.GetById(pageId);
            return Translation(htmlHelper, page, context.Site);
        }

        public static Dictionary<string, string> TranslationUrls(this IHtmlHelper htmlHelper)
        {
            var context = htmlHelper.Panther();
            var dict= new Dictionary<string, string>();
            foreach (var pageId in context.Current.Translations)
            {
                var page = context.Root.GetById(pageId);
                dict.Add(page.Culture, ("http://" + context.Site.Url + page.Path).TrimEnd('/'));
            }
            return dict;
        }

        public static HtmlString Translation(this IHtmlHelper htmlHelper, Page page, Site site)
        {
            var builder = new TagBuilder("link");
            //<link rel="alternate" href="http://example.com/en-ie" hreflang="en-ie" />
            builder.Attributes.Add("rel", "alternate");
            builder.Attributes.Add("href", ("http://" + site.Url + page.Path).TrimEnd('/'));
            builder.Attributes.Add("hreflang", page.Culture.ToLowerInvariant());

            return builder.ToHtmlString(TagRenderMode.SelfClosing);
        }

        public static Page TranslationRoot(this IHtmlHelper htmlHelper)
        {
            var context = htmlHelper.Panther();
            var current = context.Current;
            while (current.Parent != null && current.Parent.Culture == current.Culture)
            {
                current = current.Parent;
            }
            return current;
        }

        public static string Resource(this IHtmlHelper helper, string key)
        {
            var store = helper.ViewContext.HttpContext.ApplicationServices.GetService<IResourceStore>();

            return store.GetResource(key);

        }

    }
}

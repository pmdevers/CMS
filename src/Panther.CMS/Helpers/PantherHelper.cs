using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNet.Html.Abstractions;
using Microsoft.Framework.DependencyInjection;
using Microsoft.AspNet.Mvc.Abstractions;

using Panther.CMS.Entities;
using Panther.CMS.Interfaces;

namespace Microsoft.AspNet.Mvc.Rendering
{
    public static partial class PantherHelper
    {
        public static IPantherContext Panther(this IHtmlHelper helper)
        {
            return helper.ViewContext.HttpContext.ApplicationServices.GetService<IPantherContext>();
        }

        public static IPantherContext Panther<TModel>(this IHtmlHelper<TModel> helper)
        {
            return helper.ViewContext.HttpContext.ApplicationServices.GetService<IPantherContext>();
        }

        public static bool Evaluate(this IHtmlHelper helper, string contentName, string value)
        {
            return RenderContent(helper, contentName).ToString().Equals(value);
        }

        public static bool Evaluate<TModel>(this IHtmlHelper<TModel> helper, string contentName, string value)
        {
            return RenderContent(helper, contentName).ToString().Equals(value);
        }

        public static IHtmlContent Render(this IHtmlHelper helper, string contentName)
        {
            return RenderContent(helper, contentName);
        }

        public static IHtmlContent Render<TModel>(this IHtmlHelper<TModel> helper, string contentName)
        {
            return RenderContent(helper, contentName);
        }

        public static IHtmlContent IsCurrentPage(this IHtmlHelper helper, Page page, string returnValue)
        {
            return helper.ViewContext.HttpContext.Request.Path.Value == page.Path 
                ? new HtmlString(returnValue) 
                : new HtmlString(string.Empty);
        }

        private static IHtmlContent RenderContent(IHtmlHelper helper, string contentName)
        {
            var content = helper.ViewData.Model as Content;
            IHtmlContent result = null;

            // Find in current Content
            if (content != null)
                result = FindContent(helper, content.Children, contentName);

            if (result == null)
            {
                result = GetContent(helper, contentName);
            }

            return result ?? new HtmlString(string.Empty);
        }

        private static IHtmlContent GetContent(IHtmlHelper helper, string contentName)
        {
            IHtmlContent result = null;
            var context = Panther(helper);
            if (context != null)
            {
                //Find Page Content
                result = FindContent(helper, context.Current.Contents, contentName) ??
                         FindContent(helper, context.Site.Contents, contentName);
            }

            return result;
        }

        private static IHtmlContent FindContent(IHtmlHelper helper, IList<Content> children, string contentName)
        {
            var child = children.FirstOrDefault(x => x.Name.ToLower() == contentName.ToLower());
            if (child == null)
                return null;

            return !child.Children.Any() 
                ? new HtmlString(child.Data) 
                : helper.PartialAsync(child.Type, child).Result;
        }
    }
}
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Framework.DependencyInjection;

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

        public static HtmlString Render(this IHtmlHelper helper, string contentName)
        {
            return RenderContent(helper, contentName);
        }

        public static HtmlString Render<TModel>(this IHtmlHelper<TModel> helper, string contentName)
        {
            return RenderContent(helper, contentName);
        }

        public static HtmlString IsCurrentPage(this IHtmlHelper helper, Page page, string returnValue)
        {
            if (helper.ViewContext.HttpContext.Request.Path.Value == page.Path)
            {
                return new HtmlString(returnValue);
            }

            return new HtmlString(string.Empty);
        }

        private static HtmlString RenderContent(IHtmlHelper helper, string contentName)
        {
            var content = helper.ViewData.Model as Content;
            HtmlString result = null;

            // Find in current Content
            if (content != null)
                result = FindContent(helper, content.Children, contentName);

            if (result == null)
            {
                result = GetContent(helper, contentName);
            }

            if (result == null)
            {
                result = new HtmlString(string.Empty);
            }

            return result;
        }

        private static HtmlString GetContent(IHtmlHelper helper, string contentName)
        {
            HtmlString result = null;
            var context = Panther(helper);
            if (context != null)
            {
                //Find Page Content
                result = FindContent(helper, context.Current.Contents, contentName);

                if (result == null)
                {
                    //Find Site Content
                    result = FindContent(helper, context.Site.Contents, contentName);
                }
            }

            return result;
        }

        private static HtmlString FindContent(IHtmlHelper helper, IList<Content> children, string contentName)
        {
            var child = children.FirstOrDefault(x => x.Name.ToLower() == contentName.ToLower());
            if (child == null)
                return null;

            if (!child.Children.Any())
                return new HtmlString(child.Data);

            return helper.PartialAsync(child.Type, child).Result;
        }
    }
}
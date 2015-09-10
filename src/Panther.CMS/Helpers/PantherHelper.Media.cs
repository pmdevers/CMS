using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNet.Html.Abstractions;
using Microsoft.Framework.DependencyInjection;

using Panther.CMS.Services.Media;
using Panther.CMS.Storage.Media;

namespace Microsoft.AspNet.Mvc.Rendering
{
    public static partial class PantherHelper
    {
        private static string LorumPixelUrl = "http://lorempixel.com";

        public static IHtmlContent MediaUrl(this IHtmlHelper helper, int width, int height, string name, bool gray = false,
            int imagenr = 0, string text = "")
        {
            var url = CreateUrl("/api/media", width, height, false, name, 0, text);
            return new HtmlString(url);
        }

        public static IHtmlContent LoremPixel(this IHtmlHelper helper, int width, int height, bool gray = false,
            string category = "", int imagenr = 0, string text = "")
        {
            var url = CreateUrl(LorumPixelUrl, width, height, gray, category, imagenr, text);
            return new HtmlString(url);
        }

        public static IHtmlContent ImageMedia(this IHtmlHelper helper, string name)
        {
            var mediaService = helper.ViewContext.HttpContext.ApplicationServices.GetService<IMediaService>();
            var media = mediaService.Get(name);
            var builder = new TagBuilder("img");
            builder.Attributes.Add("src", media.Path);
            builder.Attributes.Add("alt", media.Description);
            builder.TagRenderMode = TagRenderMode.SelfClosing;

            return builder;
        }

        private static string CreateUrl(string host, int width, int height, bool gray = false,
            string category = "", int imagenr = 0, string text = "")
        {
            var sb = new StringBuilder();
            sb.Append(host);
            if (gray)
                sb.Append("/g");
            sb.Append("/" + width + "/" + height);
            if (!string.IsNullOrWhiteSpace(category))
            {
                sb.Append("/" + category);
                if (imagenr > 0)
                    sb.Append("/" + imagenr);
                if (!string.IsNullOrWhiteSpace(text))
                    sb.Append("/" + text);
            }
            return sb.ToString().ToLower();
        }
    }
}

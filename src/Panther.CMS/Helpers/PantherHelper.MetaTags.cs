using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Panther.CMS;
using Panther.CMS.Extensions;
using Panther.CMS.Interfaces;
using Panther.CMS.PageProperties;

namespace Microsoft.AspNet.Mvc.Rendering
{
    public static partial class PantherHelper
    {
        public static HtmlString MetaInformation(this IHtmlHelper htmlHelper)
        {
            var context = htmlHelper.Panther();
            var siteMeta = context.Site.GetProperties<MetaTagsProperties>();
            var metaInfo = context.Current.GetProperties<MetaTagsProperties>();

            var dictionary = metaInfo.GetDictionary();

            dictionary.AddWhereNotIn(siteMeta.GetDictionary());

            var sb = new StringBuilder();

            foreach (var tag in dictionary)
            {
                sb.AppendLine(CreateMeta(tag.Key, tag.Value).ToString());
            }
            return new HtmlString(sb.ToString());
        }

        public static HtmlString CreateMeta(string name, string content)
        {
            var builder = new TagBuilder("meta");
            builder.Attributes.Add("name", name.ToLower());
            builder.Attributes.Add("content", content);
            return builder.ToHtmlString(TagRenderMode.StartTag);
        }
    }

    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNet.Mvc.Rendering;

using Panther.CMS.PageProperties;

namespace Microsoft.AspNet.Mvc.Rendering
{
    public static partial class PantherHelper
    {
        public static HtmlString Analytics(this IHtmlHelper htmlHelper)
        {
            var context = htmlHelper.Panther();
            var siteProperty = context.Site.GetProperties<GoogleAnalyticsProperties>();
            var pageProperty = context.Current.GetProperties<GoogleAnalyticsProperties>();
            var code = string.IsNullOrEmpty(pageProperty.Id) ? siteProperty.Id : pageProperty.Id;

            if (!string.IsNullOrEmpty(code) && (siteProperty.Enabled || pageProperty.Enabled))
            {
                return new HtmlString(@"<script>
                    (function(b, o, i, l, e, r){
                        b.GoogleAnalyticsObject = l; b[l] || (b[l] =
                        function(){ (b[l].q = b[l].q ||[]).push(arguments)}); b[l].l = +new Date;
                        e = o.createElement(i); r = o.getElementsByTagName(i)[0];
                        e.src = 'https://www.google-analytics.com/analytics.js';
                        r.parentNode.insertBefore(e, r)}
                    (window, document,'script','ga'));
                    ga('create', '" + code + "', 'auto'); ga('send', 'pageview');</script>");
            }

            return new HtmlString("");
        }
    }
}

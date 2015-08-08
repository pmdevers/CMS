using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Framework.DependencyInjection;

using Panther.CMS.Attributes;
using Panther.CMS.Controllers;
using Panther.CMS.Entities;
using Panther.CMS.Extensions;
using Panther.CMS.Interfaces;
using Panther.CMS.Models;
using Panther.CMS.PageProperties;
using Panther.CMS.Services.Page;

namespace Panther.CMS
{
    public class PantherSiteMapMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IPantherContext context;
        
        public PantherSiteMapMiddleware(RequestDelegate next,
            IServiceProvider serviceProvider,
            IPantherContext context
            )
        {
            this.next = next;
            this.context = context;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path.Value.Contains("sitemap.xml"))
            {
                var pages = context.Root;
                var sitemap = new GoogleSiteMap();

                BuildSitemap(sitemap, pages);
                httpContext.Response.ContentType = "text/xml";
                await httpContext.Response.WriteAsync(sitemap.GetXMLString(), Encoding.UTF8);
                return;
            }

            await next.Invoke(httpContext);
        }

        private void BuildSitemap(GoogleSiteMap sitemap, Page page)
        {
            AddPage(sitemap, page);
            if (page.Children.Any())
                    page.Children.ToList().ForEach(x=> BuildSitemap(sitemap, x)); 
            
        }

        private void AddPage(GoogleSiteMap sitemap, Page page)
        {
            var url = CreateUrl(page);
            var properties = page.GetProperties<GoogleSitemapProperties>();
            var links = GetLnks(page);
            sitemap.Create(url, properties.LastModified, properties.Priority, properties.ChangeFrequency, links);
        }

        private string CreateUrl(Page page)
        {
            var protocol = context.Request.IsHttps ? "https://" : "http://";
            var url = string.Concat(protocol, context.Site.Url, page.Path).TrimEnd(new[] { '/' });
            return url;
        }

        private IEnumerable<GoogleSiteMap.MapNodeLink> GetLnks(Page page)
        {
            var links = new List<GoogleSiteMap.MapNodeLink>();
            if (!page.Translations.Any())
                return links;
            
            var thisLink = MapNodeLink(page);
            links.Add(thisLink);

            foreach (var pageId in page.Translations)
            {
                var linkedPage = context.Root.GetById(pageId);
                var nodeLink = MapNodeLink(linkedPage);
                links.Add(nodeLink);
            }
            return links;
        }

        private GoogleSiteMap.MapNodeLink MapNodeLink(Page page)
        {
            var nodeLink = new GoogleSiteMap.MapNodeLink
            {
                Href = CreateUrl(page),
                HrefLang = page.Culture,
                Rel = "alternate"
            };
            return nodeLink;
        }
    }
}

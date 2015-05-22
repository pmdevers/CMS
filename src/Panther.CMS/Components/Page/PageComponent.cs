using System;
using System.Collections.Generic;
using System.Linq;

using Panther.CMS.Extensions;
using Panther.CMS.Interfaces;
using Panther.CMS.Storage.Page;
using Panther.CMS.Storage.PageDefinition;
using Panther.CMS.Storage.Site;

namespace Panther.CMS.Components.Page
{
    public class PageComponent : BusinessComponent, IPageComponent
    {
        public PageComponent(IPantherContext context) : base(context)
        {
        }

        public IEnumerable<Entities.Page> GetRoot()
        {
            return GetAll().MakeTree();
        }

        public IEnumerable<Entities.Page> GetAll()
        {
            var pageDefinitionStore = new PageDefinitionStore(Context.FileSystem);
            var siteStore = new SiteStore(Context.FileSystem);
            var pageStore = new PageStore(Context.FileSystem);
            var site = siteStore.GetSite(Context.HostString);
            var pages = pageStore.GetForSite(site);

            if (!pages.Any())
            {
                var page = new Entities.Page {Name = "Home", Url = "", SiteId = site.Id, Template = "Index"};
                pageStore.Add(page);
                pages = new List<Entities.Page> {page};
            }
            return pages;
        }

        public Entities.Page GetPage(Entities.Page root, string url)
        {
            //if (Context.FileSystem.FileExists(url)) return null;

            if (string.IsNullOrEmpty(url))
                url = string.Empty;

            var urls = url.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var depth = 0;
            var current = root;
            while (current.Children.Any())
            {
                if (depth >= urls.Length)
                    break;

                foreach (var child in current.Children)
                {
                    if (child.Url.ToLower() == urls[depth].ToLower())
                    {
                        current = child;
                        depth++;
                        break;
                    }
                }
                depth++;
            }
            return current;
        }

        public void Add(Entities.Page page)
        {
            var pageStore = new PageStore(Context.FileSystem);
            if (pageStore.FindAll(x => x.SiteId == Context.Site.Id && x.ParentId == page.ParentId && x.Url == page.Url).Any())
            {
                throw new Exception(string.Format("Page with url: {0} already exists.", page.Url));
            }

            //set the siteId of the current url
            page.SiteId = Context.Site.Id;

            pageStore.Add(page);
        }

        public Entities.Page GetPage(Guid id)
        {
            var pageStore = new PageStore(Context.FileSystem);
            return pageStore.GetByKey(id);
        }
    }
}
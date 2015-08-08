using System;
using System.Collections.Generic;
using System.Linq;

using Panther.CMS.Components.Content;
using Panther.CMS.Entities;
using Panther.CMS.Extensions;
using Panther.CMS.Interfaces;
using Panther.CMS.Storage.Content;
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
            var pageStore = new PageStore(Context.FileSystem);
            var pages = pageStore.GetForSite(Context.Site);

            if (!pages.Any())
            {
                var page = new Entities.Page {Name = "Home", Url = "", SiteId = Context.Site.Id, Template = "Index"};
                pageStore.Add(page);

                AddDefaultContent(page);
                pages = new List<Entities.Page> {page};
            }
            return pages;
        }

        private void AddDefaultContent(Entities.Page page)
        {
            var pageDefinitionStore = new PageDefinitionStore(Context.FileSystem);
            var contentComponent = new ContentComponent(Context);
            var definition = pageDefinitionStore.FindAll(x => x.Name == page.Template).FirstOrDefault() ??
                             CreateDefinition(pageDefinitionStore, page.Template);

            foreach (var content in definition.Items)
            {
                contentComponent.SaveContentTree(page, content, null);
            }

        }

        private PageDefinition CreateDefinition(PageDefinitionStore pageDefinitionStore, string name)
        {
            var def = new PageDefinition();
            def.Name = name;
            
            var container = new ContentItem { Name = "container", Type = "container"};
            var row = new ContentItem { Name = "row1", Type = "row" };
            var column = new ContentItem { Name = "col-1", Type = "column" };

            row.Items.Add(column);
            container.Items.Add(row);
            def.Items.Add(container);

            pageDefinitionStore.Add(def);

            return def;
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
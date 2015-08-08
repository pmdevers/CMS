using System;
using System.Collections.Generic;
using System.Linq;

using Panther.CMS.Components.Content;
using Panther.CMS.Components.Page;
using Panther.CMS.Interfaces;

namespace Panther.CMS.Services.Page
{
    public class PageService : BaseService, IPageService
    {
        public PageService(IPantherContext context) : base(context)
        {
        }

        public Entities.Page GetPage(Entities.Page root, string value)
        {
            //var component = new PageComponent(Context);
            var content = new ContentComponent(Context);
            var page = root.GetByUrl(value) ?? root;
            page.Contents = content.GetPageContent(page);

            return page;
        }

        public Entities.Page GetRoot()
        {
            var component = new PageComponent(Context);
            return component.GetRoot().First();
        }

        public IEnumerable<Entities.Page> Get()
        {
            var component = new PageComponent(Context);
            return component.GetAll();
        }

        public void AddContentToPage(Entities.Page page, Entities.Content content)
        {
            var component = new ContentComponent(Context);
            component.AddContent(page, content);
        }

        public Entities.Page GetCurrentPage(Entities.Page root)
        {
            return GetPage(root, Context.Path);
        }

        public Entities.Page Get(Guid id)
        {
            var pageComponent = new PageComponent(Context);
            return pageComponent.GetPage(id);
        }

        public void Post(Entities.Page page)
        {
            var pageComponent = new PageComponent(Context);

            pageComponent.Add(page);
        }
    }
}
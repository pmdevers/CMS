using System.Collections.Generic;
using System.Linq;

using Panther.CMS.Entities;
using Panther.CMS.Extensions;
using Panther.CMS.Interfaces;
using Panther.CMS.Storage.Content;

namespace Panther.CMS.Components.Content
{
    public class ContentComponent : BusinessComponent
    {
        public ContentComponent(IPantherContext context) : base(context)
        {
        }

        public List<Entities.Content> GetPageContent(Entities.Page page)
        {
            var store = new ContentStore(Context.FileSystem);

            //store.Add(new Content() { Name = "Test", PageId = page.Id, Type = "string", Data = "test" });

            return store.GetAll(page).MakeTree().ToList();
        }

        public List<Entities.Content> GetSiteContent(Entities.Site site)
        {
            var store = new ContentStore(Context.FileSystem);
            return store.GetSiteContent(site).MakeTree().ToList();
        }

        public void DeleteContent(Entities.Page page)
        {
            var store = new ContentStore(Context.FileSystem);
            store.Delete(x => x.PageId == page.Id);
        }

        public void AddContent(Entities.Page page, Entities.Content contentTree)
        {
            var store = new ContentStore(Context.FileSystem);
            SaveContent(page, contentTree);
        }

        private void SaveContent(Entities.Page page, Entities.Content contentTree)
        {
            contentTree.PageId = page.Id;
            SaveContent(contentTree);

            foreach (var item in contentTree.Children)
            {
                item.ParentId = contentTree.Id;
                SaveContent(page, item);
            }
        }

        private void SaveContent(Entities.Content content)
        {
            var store = new ContentStore(Context.FileSystem);
            store.Add(content);
        }

        public void SaveContentTree(Entities.Page page, ContentItem content, Entities.Content parent)
        {
            var newParent = new Entities.Content { PageId = page.Id, ParentId = parent?.Id, Name = content.Name, Type = content.Type, Data = content.Data };
            SaveContent(newParent);
            content.Items.ForEach(x=>SaveContentTree(page, x, newParent));
        }
    }
}
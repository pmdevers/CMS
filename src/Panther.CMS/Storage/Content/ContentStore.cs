using System;
using System.Collections.Generic;

using Panther.CMS.Interfaces;

namespace Panther.CMS.Storage.Content
{
    public class ContentStore : Store<Entities.Content, Guid>, IContentStore
    {
        public ContentStore(IPantherFileSystem fileSystem) : base(fileSystem)
        { }

        public override Guid GenerateKey()
        {
            return Guid.NewGuid();
        }

        public IEnumerable<Entities.Content> GetAll(Entities.Page page)
        {
            return FindAll(x => x.PageId == page.Id);
        }

        public IEnumerable<Entities.Content> GetSiteContent(Entities.Site site)
        {
            return FindAll(x => x.SiteId == site.Id);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

using Panther.CMS.Interfaces;

namespace Panther.CMS.Storage.Page
{
    public class PageStore : Store<Entities.Page, Guid>, IPageStore
    {
        public PageStore(IPantherFileSystem fileSystem) : base(fileSystem)
        { }

        public override Guid GenerateKey()
        {
            return Guid.NewGuid();
        }

        public Entities.Page GetByUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                url = string.Empty;

            var page = FindAll(x => x.Url == url).FirstOrDefault();

            return page;
        }

        public IEnumerable<Entities.Page> GetForSite(Entities.Site site)
        {
            return FindAll(x => x.SiteId == site.Id);
        }
    }
}
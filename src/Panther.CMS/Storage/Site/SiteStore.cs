using System;
using System.Linq;

using Panther.CMS.Interfaces;

namespace Panther.CMS.Storage.Site
{
    public class SiteStore : Store<Entities.Site, Guid>, ISiteStore
    {
        public SiteStore(IPantherFileSystem fileSystem) : base(fileSystem)
        {
        }

        public override Guid GenerateKey()
        {
            return Guid.NewGuid();
        }

        public Entities.Site GetSite(string url)
        {
            Entities.Site site = FindAll(x => url.StartsWith(x.Url)).OrderByDescending(x=>x.Url.Length).FirstOrDefault() ?? CreateNewSite(url);

            return site;
        }

        private Entities.Site CreateNewSite(string hostString)
        {
            var site = new Entities.Site { Url = hostString, Name = hostString };
            this.Add(site);
            this.Save();
            return site;
        }
    }
}
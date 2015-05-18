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

        public Entities.Site GetSite(string hostString)
        {
            Entities.Site site = FindAll(x => x.Url == hostString).FirstOrDefault() ?? CreateNewSite(hostString);

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
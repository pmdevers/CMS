using System;

using Panther.CMS.Interfaces;

namespace Panther.CMS.Storage.Site
{
    public interface ISiteStore : IStore<Entities.Site, Guid>
    {
        Entities.Site GetSite(string hostString);
    }
}
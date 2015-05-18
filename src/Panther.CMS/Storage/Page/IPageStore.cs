using System;
using System.Collections.Generic;

using Panther.CMS.Interfaces;

namespace Panther.CMS.Storage.Page
{
    public interface IPageStore : IStore<Entities.Page, Guid>
    {
        Entities.Page GetByUrl(string url);

        IEnumerable<Entities.Page> GetForSite(Entities.Site site);
    }
}
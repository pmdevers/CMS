using System;
using System.Collections.Generic;

using Panther.CMS.Interfaces;

namespace Panther.CMS.Storage.Content
{
    public interface IContentStore : IStore<Entities.Content, Guid>
    {
        IEnumerable<Entities.Content> GetAll(Entities.Page page);

        IEnumerable<Entities.Content> GetSiteContent(Entities.Site site);
    }
}
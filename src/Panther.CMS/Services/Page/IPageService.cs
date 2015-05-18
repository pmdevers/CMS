using System;
using System.Collections.Generic;

namespace Panther.CMS.Services.Page
{
    public interface IPageService
    {
        IEnumerable<Entities.Page> Get();

        Entities.Page GetRoot();

        Entities.Page GetPage(Entities.Page root, string value);

        Entities.Page Get(Guid id);

        void Post(Entities.Page page);
    }
}
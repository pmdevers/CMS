using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Panther.Hal;

namespace Panther.CMS.Controllers.Api
{
    public static partial class LinkTemplates
    {
        public static Link GetPage()
        {
            return Link.CreateLink("page", "/api/page/{0}");
        }

        public static Link GetPages()
        {
            return Link.CreateLink("pages", "/api/site/pages");
        }
    }
}

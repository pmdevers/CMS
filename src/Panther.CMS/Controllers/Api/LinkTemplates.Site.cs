using Panther.Hal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Panther.CMS.Controllers.Api
{
    public partial class LinkTemplates
    {
        public static Link GetSite()
        {
            return Link.CreateSelfLink("/api/site");
        }
    }
}

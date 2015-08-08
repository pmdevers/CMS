using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Panther.CMS.Interfaces;

namespace Panther.CMS.SiteProperties
{
    public class ResponseHeader : ISiteProperty
    {
        public string Description { get; set; }
        public string Version { get; set; }
    }
}

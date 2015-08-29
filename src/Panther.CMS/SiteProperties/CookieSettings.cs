using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Panther.CMS.Interfaces;

namespace Panther.CMS.SiteProperties
{
    public class CookieSettings : ISiteProperty
    {
        [DefaultValue(360)]
        public int Duration { get; set; }
        [DefaultValue(false)]
        public bool IsHttp { get; set; }
    }
}

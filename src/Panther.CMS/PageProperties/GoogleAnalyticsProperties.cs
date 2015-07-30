using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Panther.CMS.Interfaces;

namespace Panther.CMS.PageProperties
{
    public class GoogleAnalyticsProperties : IPageProperty, ISiteProperty
    {
        public string Id { get; set; }
        public bool Enabled { get; set; }
    }
}

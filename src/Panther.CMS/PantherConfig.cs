using System;

using Microsoft.AspNet.Routing;
using Microsoft.Framework.ConfigurationModel;

namespace Panther.CMS
{
    public class PantherConfig : IPantherConfig
    {
        public string Test { get; set; }
        public IConfiguration Configuration { get; set; }

        public Action<IRouteBuilder> Routes { get; set; }
        public string LoginPath { get; set; }
    }
}
using System;

using Microsoft.AspNet.Routing;

namespace Panther.CMS
{
    public class PantherConfig : IPantherConfig
    {
        public void Test2()
        {
        }

        public PantherConfig()
        {
        }

        public string Test { get; set; }

        public Action<IRouteBuilder> Routes { get; set; }
    }
}
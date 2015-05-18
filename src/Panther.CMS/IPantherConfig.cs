using System;

using Microsoft.AspNet.Routing;

namespace Panther.CMS
{
    public interface IPantherConfig
    {
        string Test { get; set; }

        Action<IRouteBuilder> Routes { get; set; }
    }
}
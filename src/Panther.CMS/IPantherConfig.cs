using System;

using Microsoft.AspNet.Routing;
using Microsoft.Framework.ConfigurationModel;

namespace Panther.CMS
{
    public interface IPantherConfig
    {
        string Test { get; set; }

        IConfiguration Configuration { get; set; }

        Action<IRouteBuilder> Routes { get; set; }
    }
}
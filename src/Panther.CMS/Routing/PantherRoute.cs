using System.Collections.Generic;
using System.Diagnostics;

using Panther.CMS.Interfaces;

namespace Panther.CMS.Routing
{
    public class PantherRouter : IPantherRouter
    {
        public PantherRouter()
        {
        }

        public void AddVirtualRouteValues(string route, string virtualPath, IDictionary<string, object> values)
        {
            var routeInfo = new RouteInfo(route);

            var routeValues = routeInfo.ParseRoute(virtualPath);

            if (routeValues == null)
                return;

            foreach (var value in routeValues)
            {
                if (values.ContainsKey(value.Key))
                {
                    values[value.Key] = value.Value;
                }
                else
                {
                    values.Add(value);
                }
            }

            Debug.Write(routeInfo.ToString());
        }
    }
}
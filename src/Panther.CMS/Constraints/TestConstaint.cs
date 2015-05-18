using System.Collections.Generic;

using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;

namespace Panther.CMS.Constraints
{
    public class TestConstaint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, IDictionary<string, object> values, RouteDirection routeDirection)
        {
            return true;
        }
    }
}
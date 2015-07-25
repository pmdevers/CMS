using System.Collections.Generic;
using System.Globalization;
using System.Threading;

using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;
using Microsoft.Framework.DependencyInjection;

using Panther.CMS.Interfaces;

namespace Panther.CMS.Constraints
{
    /// <summary>
    /// Summary description for RouteConstraint
    /// </summary>
    public class HostConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, IDictionary<string, object> values, RouteDirection routeDirection)
        {
            var context = httpContext.ApplicationServices.GetService<IPantherContext>();
            var url = "/";


            if (context == null)
                return false;

            //if (values.ContainsKey("culture") && !CheckCulture(values["culture"].ToString()))
            //    return false;
           if(values.ContainsKey("url") && values["url"] != null)
                url = values["url"].ToString();

            var canHandle = context.CanHandleUrl(context.Path);

            if (!canHandle)
                return false;

            if (!string.IsNullOrEmpty(context.Current.Controller))
                values["controller"] = context.Current.Controller;

            if (!string.IsNullOrEmpty(context.Current.Action))
                values["action"] = context.Current.Action;

            if (!string.IsNullOrEmpty(context.Current.Route))
                context.Router.AddVirtualRouteValues(context.Current.Route, context.VirtualPath, values);

            values["context"] = context;

            return context.Current != null;
        }

        private bool CheckCulture(string culture)
        {
            try
            {

            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
using System.Collections.Generic;

namespace Panther.CMS.Interfaces
{
    public interface IPantherRouter
    {
        void AddVirtualRouteValues(string route, string virtualPath, IDictionary<string, object> values);
    }
}
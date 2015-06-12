using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNet.Http;

namespace Panther.CMS.Extensions
{
    public static class RequestExtensions
    {
        public static bool IsApiRequest(this HttpRequest request)
        {
            return request.Path.Value.StartsWith("/api");
        }
    }
}

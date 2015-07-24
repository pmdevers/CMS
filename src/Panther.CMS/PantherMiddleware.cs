using System;
using System.Threading.Tasks;

using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;

using Panther.CMS.Interfaces;

namespace Panther.CMS
{
    public class PantherMiddleware
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly RequestDelegate _next;
        private readonly IPantherContext _context;

        public PantherMiddleware(RequestDelegate next,
                IServiceProvider serviceProvider,
                IPantherContext context
            )
        {
            _next = next;
            _serviceProvider = serviceProvider;
            _context = context;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var currentApplicationService = httpContext.ApplicationServices;
            var currentRequestServices = httpContext.RequestServices;
            
            _context.Initialize(httpContext);
            await _next.Invoke(httpContext);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNet.Mvc;
using Microsoft.Framework.DependencyInjection;

using Panther.CMS.Entities;
using Panther.CMS.Interfaces;

namespace Panther.CMS.Filters
{
    public class SecurityFilterAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(AuthorizationContext context)
        {
            var pantherContext = context.HttpContext.RequestServices.GetService<IPantherContext>();
            var page = pantherContext.Current;

            if (HasSecurity(page))
            {
                
                var user = context.HttpContext.User;

                var userIsAnonymous =
                    user == null ||
                    user.Identity == null ||
                    !user.Identity.IsAuthenticated;

                if (userIsAnonymous && !UserIsAuthorized(page, user))
                {
                    base.Fail(context);
                }
            }
        }

        private bool UserIsAuthorized(Page page, ClaimsPrincipal user)
        {
            return page.RequiredRoles.Any(user.IsInRole);
        }

        public bool HasSecurity(Page page)
        {
            return page != null && !page.AllowAnonymous;
        }
    }
}

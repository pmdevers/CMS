using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNet.Http;
using Microsoft.AspNet.Loader.IIS;

using Panther.CMS.Interfaces;
using Panther.CMS.SiteProperties;

namespace Panther.CMS.Helpers
{
    public class Cookie
    {
        private readonly CookieSettings settings;
        private readonly IPantherContext context;
        
        public Cookie(IPantherContext context)
        {
        
            this.context = context;
            this.settings = context.Site.GetProperties<CookieSettings>();
        }

        public void Set(string key, string value)
        {
            var c = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(settings.Duration),
                HttpOnly = settings.IsHttp
            };
            context.Response.Cookies.Append(key, value, c);
        }

        public string Get(string key)
        {
            var value = string.Empty;

            var c = context.Request.Cookies[key];
            return c ?? value;
        }

        public bool Exists(string key)
        {
            return context.Request.Cookies[key] != null;
        }

        public void Delete(string key)
        {
            if (Exists(key))
            {
                var c = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(-1),
                    HttpOnly = settings.IsHttp
                };
                context.Response.Cookies.Append(key, string.Empty, c);
            }
        }

        public void DeleteAll()
        {
            foreach (var key in context.Request.Cookies.Keys)
            {
                Delete(key);
            }

        }
    }
}

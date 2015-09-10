using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

using Microsoft.AspNet.Http;

using Panther.CMS.Entities;
using Panther.CMS.Filters;
using Panther.CMS.Interfaces;
using Panther.CMS.Services.Page;
using Panther.CMS.Services.Site;
using Microsoft.Dnx.Runtime;
using Microsoft.Framework.Caching.Memory;

using Panther.CMS.Helpers;
using Panther.CMS.SiteProperties;

namespace Panther.CMS
{
    public class PantherContext : IPantherContext, IDisposable
    {
        private HttpContext context;
        private readonly ISiteService siteService;
        private readonly IPageService pageService;
        private IMemoryCache cache;
        public Site Site { get; private set; }

        public Page Current { get; private set; }

        public Page Root { get; private set; }

        public Page Refferral { get; private set; }

        public IPantherFileSystem FileSystem { get; private set; }

        public IServiceProvider Services { get; private set; }

        public IPantherRouter Router { get; private set; }

        public Cookie Cookie { get; private set; }

        public CultureInfo Culture
        {
            get { return CultureInfo.CurrentUICulture; }
        }

        public PantherContext(IServiceProvider services, IPantherRouter router, IPantherFileSystem fileSystem, IApplicationEnvironment appEnv)
        {
            this.appEnv = appEnv;
            Services = services;
            FileSystem = fileSystem;
            Router = router;
            this.cache = new MemoryCache(new MemoryCacheOptions());
            this.siteService = new SiteService(this);
            this.pageService = new PageService(this);
        }

        public void Initialize(HttpContext context)
        {
            this.context = context;
            Site = siteService.GetSite();
            Root = pageService.GetRoot();
            Refferral = pageService.GetPage(Root, RefferralString);
            Cookie = new Cookie(this);

            var responseHeader = Site.GetProperties<ResponseHeader>();
            var response = context.Response;
            response.Headers["x-" + Site.Name.ToLower() + "-description"] = responseHeader.Description;
            response.Headers["x-software"] = "Panther Content Management System.";
            response.Headers["x-version"] = appEnv.ApplicationVersion;
        }

        private void SetCulture()
        {
            var cul = new CultureInfo(Current.Culture);

#if DNXCORE50
            CultureInfo.CurrentCulture = cul;
            CultureInfo.CurrentUICulture = cul;
#else
            Thread.CurrentThread.CurrentCulture = cul;
            Thread.CurrentThread.CurrentUICulture = cul;
#endif
        }

        public HttpRequest Request => context.Request;
        public HttpResponse Response => context.Response;

        public T GetCached<T>(string key) where T : class
        {
            var survey = cache.Get(key) as T;
            if (survey == null)
            {
                survey = Activator.CreateInstance<T>();
                SetCached(key, survey);
            }

            return survey;
        }

        public void SetCached<T>(string key, T value) where T : class
        {
            if (value == null)
            {
                cache.Remove(key);
                return;
            }
            
            cache.Set(key, value);
        }

        public bool CanHandleUrl(string url)
        {
            //url = url.TrimEnd('/');
            Current = pageService.GetPage(Root, url);

            if (Current.Path.ToLower().EndsWith(url.ToLower()))
            {
                SetCulture();
                return true;
            }
            if (string.IsNullOrEmpty(Current.Route))
                return false;

            return Current != null;
        }

        public string HostString
        {
            get
            { 
                return context.Request.Headers["Host"].ToString();
            }
        }

        public string RefferralString
        {
            get
            {
                return context.Request.Headers["referer"];
            }
        }

        public string Path
        {
            get
            {
                var path =  context.Request.Path.Value.ToLower();
                var sitePath = Site.Url.Replace(HostString, string.Empty);
                return path.Substring(sitePath.Length, path.Length - sitePath.Length);
            }
        }

        public string Url
        {
            get { return HostString + context.Request.Path.Value.ToLower(); }
        }

        public string VirtualPath
        {
            get
            {
                return Path.TrimStart(Current.Path.ToCharArray());
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        private bool _disposed = false;
        private IApplicationEnvironment appEnv;

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                }

                Services = null;
                Current = null;
                Root = null;

                _disposed = true;
            }
        }

        ~PantherContext()
        {
            Dispose();
        }
    }
}
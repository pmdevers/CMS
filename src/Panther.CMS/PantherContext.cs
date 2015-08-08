using System;
using System.Globalization;
using System.Threading;

using Microsoft.AspNet.Http;

using Panther.CMS.Entities;
using Panther.CMS.Filters;
using Panther.CMS.Interfaces;
using Panther.CMS.Services.Page;
using Panther.CMS.Services.Site;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Loader.IIS;
using Microsoft.Framework.Runtime;

using Panther.CMS.SiteProperties;

namespace Panther.CMS
{
    public class PantherContext : IPantherContext, IDisposable
    {
        private HttpContext context;
        private readonly ISiteService siteService;
        private readonly IPageService pageService;

        public Site Site { get; private set; }

        public Page Current { get; private set; }

        public Page Root { get; private set; }

        public Page Refferral { get; private set; }

        public IPantherFileSystem FileSystem { get; private set; }

        public IServiceProvider Services { get; private set; }

        public IPantherRouter Router { get; private set; }

        public PantherContext(IServiceProvider services, IPantherRouter router, IPantherFileSystem fileSystem, IApplicationEnvironment appEnv)
        {
            this.appEnv = appEnv;
            Services = services;
            FileSystem = fileSystem;
            Router = router;
            this.siteService = new SiteService(this);
            this.pageService = new PageService(this);
        }

        public void Initialize(HttpContext context)
        {
            this.context = context;

            Site = siteService.GetSite();
            Root = pageService.GetRoot();
            Refferral = pageService.GetPage(Root, RefferralString);

            var responseHeader = Site.GetProperties<ResponseHeader>();
            var response = context.Response;
            response.Headers["x-" + Site.Name.ToLower() + "-description"] = responseHeader.Description;
            response.Headers["x-software"] = "Panther Content Management System.";
            response.Headers["x-version"] = appEnv.Version;
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
using System;

using Microsoft.AspNet.Http;

using Panther.CMS.Entities;
using Panther.CMS.Filters;
using Panther.CMS.Interfaces;
using Panther.CMS.Services.Page;
using Panther.CMS.Services.Site;

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

        public PantherContext(IServiceProvider services, IPantherRouter router, IPantherFileSystem fileSystem)
        {
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
        }

        public bool CanHandleUrl(string url)
        {
            Current = pageService.GetPage(Root, url);

            if (Current.Path.ToLower().EndsWith(url.ToLower()))
                return true;

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
                return context.Request.Path.Value.ToLower();
            }
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
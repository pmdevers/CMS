using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

using Panther.CMS.Attributes;
using Panther.CMS.Entities;
using Panther.CMS.Extensions;
using Panther.CMS.Interfaces;
using Panther.CMS.Models;
using Panther.CMS.Services.Page;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Panther.CMS.Controllers
{
    public class SitemapController : Controller
    {
        

        readonly IPageService pageService;
        readonly IPantherContext context;

        public SitemapController(IPageService pageService, IPantherContext context)
        {
            this.pageService = pageService;
            this.context = context;
        }

        // GET: /<controller>/
        //public IActionResult Index()
        //{
        //    var pages = pageService.Get().MakeTree();
        //    var sitemap = new GoogleSiteMap();

        //    BuildSitemap(sitemap, pages);
            
        //    return Content(sitemap.GetXMLString(), "text/xml", Encoding.UTF8);
        //}

        
    }
}

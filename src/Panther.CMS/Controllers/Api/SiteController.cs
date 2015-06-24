using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

using Panther.CMS.Entities;
using Panther.CMS.Services.Site;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Panther.CMS.Controllers.Api
{
    public class SiteController : BaseController
    {

        private readonly ISiteService siteService;

        public SiteController(ISiteService siteService)
        {
            this.siteService = siteService;
        }
        // GET: /<controller>/
        [HttpGet]
        public Site Get()
        {
            return siteService.GetSite();
        }
    }
}

using Microsoft.AspNet.Mvc;

using Panther.CMS.Entities;
using Panther.CMS.Services.Site;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Panther.CMS.Controllers.Api
{
    [Route("api/[controller]")]
    public class SiteController : Controller
    {

        private ISiteService siteService;

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

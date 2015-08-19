using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Panther.CMS.Controllers.Api.Models;
using Panther.CMS.Entities;
using Panther.CMS.Services.Site;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Panther.CMS.Controllers.Api
{
    [Route("api/site")]
    public class SiteController
    {

        private readonly ISiteService siteService;

        public SiteController(ISiteService siteService)
        {
            this.siteService = siteService;
        }
        // GET: /<controller>/
        [HttpGet]
        public SiteRepresentation Get()
        {
            var site = siteService.GetSite();
            
            return new SiteRepresentation
            {
                SiteName = site.Name,
                Culture = site.Culture,
                Properties = site.Properties,
                Scripts = site.Scripts,
                StyleSheet = site.Stylesheets
            };
            
        }
    }
}

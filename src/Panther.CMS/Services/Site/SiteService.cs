using Panther.CMS.Components.Content;
using Panther.CMS.Components.Site;
using Panther.CMS.Interfaces;

namespace Panther.CMS.Services.Site
{
    public class SiteService : BaseService, ISiteService
    {
        public SiteService(IPantherContext context) : base(context)
        {
        }

        public Entities.Site GetSite()
        {
            var component = new SiteComponent(Context);
            var contentComponent = new ContentComponent(Context);
            var site = component.GetSite();

            var contents = contentComponent.GetSiteContent(site);

            site.Contents = contents;

            return site;
        }
    }
}
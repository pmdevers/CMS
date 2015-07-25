using Panther.CMS.Interfaces;
using Panther.CMS.Storage.Site;

namespace Panther.CMS.Components.Site
{
    public class SiteComponent : BusinessComponent, ISiteComponent
    {
        public SiteComponent(IPantherContext context) : base(context)
        {
        }

        public Entities.Site GetSite()
        {
            var store = new SiteStore(Context.FileSystem);
            var site = store.GetSite(Context.Url);
            return site;
        }
    }
}
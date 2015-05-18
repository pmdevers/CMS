using Panther.CMS.Interfaces;
using Panther.CMS.Services.Page;
using Panther.CMS.Services.Site;

namespace Panther.CMS.Services
{
    public class DefaultServiceFactory : IServiceFactory
    {
        private IPantherContext context;

        public DefaultServiceFactory(IPantherContext context)
        {
            this.context = context;
        }

        public IPageService PageServices
        {
            get
            {
                return new PageService(context);
            }
        }

        public ISiteService SiteService
        {
            get
            {
                return new SiteService(context);
            }
        }
    }
}
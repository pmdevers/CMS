using Panther.CMS.Interfaces;

namespace Panther.CMS.Services
{
    public class BaseService
    {
        protected IPantherContext Context;

        protected BaseService(IPantherContext context)
        {
            Context = context;
        }
    }
}
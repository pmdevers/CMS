using Panther.CMS.Interfaces;

namespace Panther.CMS.Components
{
    public abstract class BusinessComponent
    {
        protected IPantherContext Context;

        protected BusinessComponent(IPantherContext context)
        {
            Context = context;
        }
    }
}
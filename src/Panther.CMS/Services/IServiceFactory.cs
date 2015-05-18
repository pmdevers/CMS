using Panther.CMS.Services.Page;

namespace Panther.CMS.Services
{
    public interface IServiceFactory
    {
        IPageService PageServices { get; }
    }
}
using System.IO;

using Microsoft.AspNet.Http;

namespace Panther.CMS.Services.Media
{
    public interface IMediaService
    {
        void SaveMedia(IFormFile file, string name, string description);

        Entities.Media Get(string name);
    }
}

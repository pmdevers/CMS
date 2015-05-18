using Microsoft.AspNet.StaticFiles;

namespace Panther.CMS
{
    public class StaticFilesMimeType : FileExtensionContentTypeProvider
    {
        public StaticFilesMimeType() : base()
        {
            Mappings.Add(".woff2", "application/font-woff2");
        }
    }
}
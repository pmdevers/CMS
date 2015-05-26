using Microsoft.AspNet.StaticFiles;

namespace Panther.CMS
{
    public class StaticFilesMimeType : FileExtensionContentTypeProvider
    {
        public StaticFilesMimeType() : base()
        {
            AddExtension(".woff2", "application/font-woff2");
        }

        public void AddExtension(string extension, string mime)
        {
            if (!Mappings.ContainsKey(extension))
                Mappings.Add(extension, mime);
        }
    }
}
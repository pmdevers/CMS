using Microsoft.AspNet.Http;
using Microsoft.AspNet.Loader.IIS;

using Panther.CMS.Entities;

namespace Panther.CMS.Interfaces
{
    public interface IPantherContext
    {
        IPantherFileSystem FileSystem { get; }

        IPantherRouter Router { get; }

        string HostString { get; }

        string Path { get; }

        Page Refferral { get; }

        string VirtualPath { get; }

        Page Current { get; }

        Page Root { get; }

        Site Site { get; }
        string Url { get; }
        HttpRequest Request { get; }
        void Initialize(HttpContext context);
        bool CanHandleUrl(string url);
    }
}
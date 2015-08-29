using System.Globalization;

using Microsoft.AspNet.Http;
using Microsoft.AspNet.Loader.IIS;

using Panther.CMS.Entities;
using Panther.CMS.Helpers;

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
        HttpResponse Response { get; }
        CultureInfo Culture { get; }
        Cookie Cookie { get; }

        T GetCached<T>(string key) where T : class;

        void SetCached<T>(string key, T value) where T : class;
        void Initialize(HttpContext context);
        bool CanHandleUrl(string url);
    }
}
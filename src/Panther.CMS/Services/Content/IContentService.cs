using System.Collections.Generic;

using Panther.CMS.Services.Models;

namespace Panther.CMS.Services.Content
{
    public interface IContentService
    {
        void AddToSite(ContentModel model);

        void AddToPage(string url, List<ContentModel> model);

        List<ContentModel> GetContent(string url);
    }
}
using System.Collections.Generic;

namespace Panther.CMS.Services.Models
{
    public class ContentModel
    {
        public ContentModel()
        {
            Children = new List<ContentModel>();
        }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Data { get; set; }

        public List<ContentModel> Children { get; set; }
    }
}
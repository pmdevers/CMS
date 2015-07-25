using System.Collections.Generic;

namespace Panther.CMS.Entities
{
    public class ContentItem
    {
        public ContentItem()
        {
            Items = new List<ContentItem>();
        }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Data { get; set; }

        public List<ContentItem> Items { get; set; }
    }
}
using System;
using System.Collections.Generic;

using Panther.CMS.Interfaces;

namespace Panther.CMS.Entities
{
    public class PageDefinition : IEntity<Guid>
    {
        public PageDefinition()
        {
            Items = new List<ContentItem>();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Layout { get; set; }

        public List<ContentItem> Items { get; set; }
    }
}
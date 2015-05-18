using System;

using Panther.CMS.Interfaces;

namespace Panther.CMS.Entities
{
    public partial class Page : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public Guid? ParentId { get; set; }

        public Guid SiteId { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public string Template { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public string Route { get; set; }
    }
}
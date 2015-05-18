using System;

using Panther.CMS.Interfaces;

namespace Panther.CMS.Entities
{
    public partial class Content : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public Guid? ParentId { get; set; }

        public Guid? PageId { get; set; }

        public Guid? SiteId { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Data { get; set; }
    }
}
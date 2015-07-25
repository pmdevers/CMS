using System;

using Panther.CMS.Interfaces;

namespace Panther.CMS.Entities
{
    public partial class Site : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }
        public string Culture { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Panther.CMS.Interfaces;

namespace Panther.CMS.Entities
{
    public class Media : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public string CacheControl { get; set; }
    }
}

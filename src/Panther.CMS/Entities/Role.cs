using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Panther.CMS.Interfaces;

namespace Panther.CMS.Entities
{
    public class Role : IEntity<Guid>
    {
        public Role()
        {
            Claims = new List<RoleClaim>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ConcurrencyStamp { get; set; }
        public List<RoleClaim> Claims { get; set; }
    }

    public class RoleClaim
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Panther.CMS.Interfaces;

namespace Panther.CMS.Entities
{
    public class Resource : IEntity<string>
    {
        public string Id { get; set; }
        public string Value { get; set; }
    }
}

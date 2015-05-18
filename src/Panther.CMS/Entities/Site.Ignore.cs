using System;
using System.Collections.Generic;

using Newtonsoft.Json;

using Panther.CMS.Interfaces;

namespace Panther.CMS.Entities
{
    public partial class Site : IEntity<Guid>
    {
        public Site()
        {
            Contents = new List<Content>();
        }

        [JsonIgnore]
        public IList<Content> Contents { get; set; }
    }
}
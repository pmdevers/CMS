using System;
using System.Collections.Generic;

using Newtonsoft.Json;

using Panther.CMS.Interfaces;

namespace Panther.CMS.Entities
{
    public partial class Content : ITreeAble<Content, Guid>
    {
        public Content()
        {
            Children = new List<Content>();
        }

        [JsonIgnore]
        public IList<Content> Children { get; set; }

        [JsonIgnore]
        public Content Parent { get; set; }
    }
}
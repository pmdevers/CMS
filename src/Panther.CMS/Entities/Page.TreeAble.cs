using System;
using System.Collections.Generic;

using Newtonsoft.Json;

using Panther.CMS.Interfaces;

namespace Panther.CMS.Entities
{
    public partial class Page : ITreeAble<Page, Guid>
    {
        public Page()
        {
            Children = new List<Page>();
            Contents = new List<Content>();
            AllowAnonymous = true;
        }

        [JsonIgnore]
        public IList<Page> Children { get; set; }

        [JsonIgnore]
        public Page Parent { get; set; }

        [JsonIgnore]
        public string Path
        {
            get
            {
                string path = string.Empty;
                var parent = Parent as Page;
                if (parent != null)
                {
                    path = parent.Path;
                }

                if (!path.EndsWith("/"))
                {
                    path = path + "/";
                }

                return path + Url;
            }
        }

        [JsonIgnore]
        public IList<Content> Contents { get; set; }
    }
}
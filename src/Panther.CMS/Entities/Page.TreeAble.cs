using System;
using System.Collections.Generic;
using System.Linq;

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
            Properties = new Dictionary<string, string>();
            AllowAnonymous = true;
            Translations = new List<Guid>();
            RequiredRoles= new List<string>();
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
        [JsonIgnore]
        public bool ShowInMenu
        {
            get
            {
                var isSameCulture = Parent == null || Culture == Parent.Culture;
                return isSameCulture;
            }
        }

        public Page GetById(Guid pageId)
        {
            if (Id == pageId)
                return this;

            foreach (var page in Children)
            {
                var childPage = page.GetById(pageId);
                if (childPage != null)
                    return childPage;
            }
            return null;

        }

        public Page GetByUrl(string url = "")
        {
            if(string.IsNullOrEmpty(url))
                url = string.Empty;

            var segments = url.Split(new []{'/'}, StringSplitOptions.RemoveEmptyEntries).ToList();
            var current = this;
            for (int i = 0; i < segments.Count; i++)
            {
                var segment = segments[i];
                current = current.Children.FirstOrDefault(x => x.Url == segment);
                if (current == null)
                    return null;
            }

            return current;

            //if (Url == segment)
            //    return this;

            //foreach (var child in Children)
            //{
            //    var found = child.GetByUrl(string.Join("/", segments));

            //    if (found != null && !segments.Any())
            //    {
            //        return found;
            //    }
            //}

            //return null;
        }
    }
}
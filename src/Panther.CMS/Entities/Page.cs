using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

using Panther.CMS.Helpers;
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
        public bool AllowAnonymous { get; set; }

        public List<string> RequiredRoles { get; set; } 

        public Dictionary<string, string> Properties { get; set; }
        public string Culture { get; set; }
        public List<Guid> Canonicals { get; set; }
        public T GetProperties<T>() where T : IPageProperty
        {
            return ObjectHydrator.Build<T>(Properties, "Properties");
        }
    

        public void AddProperties(DictionaryObject obj)
        {
            ObjectHydrator.Into(obj, Properties, "Properties");
        }
    }
}
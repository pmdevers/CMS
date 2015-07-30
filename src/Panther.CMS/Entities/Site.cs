using System;
using System.Collections.Generic;

using Microsoft.Framework.WebEncoders;

using Panther.CMS.Helpers;
using Panther.CMS.Interfaces;

namespace Panther.CMS.Entities
{
    public partial class Site : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }
        public string Culture { get; set; }

        public Dictionary<string, string> Properties { get; set; }

        public List<string> Stylesheets { get; set; }
        public List<string> Scripts { get; set; }

        public T GetProperties<T>() where T: ISiteProperty
        {
            return ObjectHydrator.Build<T>(Properties, "Properties");
        }

        public void SetProperties<T>(T properties) where T : ISiteProperty
        {
            ObjectHydrator.Into(properties, Properties, "Properties");
        }

    }
}
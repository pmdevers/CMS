using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Panther.CMS.Interfaces;

namespace Panther.CMS.PageProperties
{
    public class MetaTagsProperties : DictionaryObject, IPageProperty, ISiteProperty
    {
        public string Description
        {
            get { return GetField<string>(); }
            set { SetField(value); }
        }

        public string Keywords
        {
            get { return GetField<string>(); }
            set { SetField(value); }
        }
        public string Robots
        {
            get { return GetField<string>(); }
            set { SetField(value); }
        }

        public string Viewport
        {
            get { return GetField<string>(); }
            set { SetField(value); }
        }
    }
}

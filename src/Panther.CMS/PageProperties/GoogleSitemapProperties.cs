using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Panther.CMS.Attributes;
using Panther.CMS.Interfaces;
using Panther.CMS.Models;

namespace Panther.CMS.PageProperties
{
    public class GoogleSitemapProperties : DictionaryObject, IPageProperty
    {
        [DefaultValue(typeof(ChangeFrequency), "Daily")]
        public ChangeFrequency ChangeFrequency
        {
            get { return GetField<ChangeFrequency>(); }
            set { SetField(value); }
        }

        [DefaultDate("")]
        public DateTime LastModified
        {
            get { return GetField<DateTime>(); }
            set { SetField(value); }
        }
        [DefaultValue("1.0")]
        public string Priority
        {
            get { return GetField<string>(); }
            set { SetField(value); }
        }
    }
}

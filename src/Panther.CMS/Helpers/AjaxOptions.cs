using Microsoft.AspNet.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Mvc.Rendering
{
    public class AjaxOptions
    {
        public AjaxOptions()
        {
            FormMethod = FormMethod.Post;
            InsertionMode = InsertionMode.Replace;
        }

        public string JQuerySelector { get; set; }
        public string Url { get; set; }
        public FormMethod FormMethod { get; set; }

        public InsertionMode InsertionMode { get; set; }
    }

    public enum InsertionMode
    {
        InsertBefore,
        InsertAfter,
        Replace,
        ReplaceWith
    }
}

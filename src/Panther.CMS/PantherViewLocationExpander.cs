using System;
using System.Collections.Generic;

using Microsoft.AspNet.Mvc.Razor;

namespace Panther.CMS
{
    public class PantherViewLocationExpander : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            return new List<string>(viewLocations) { "/Views/Templates/{0}.cshtml" };
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
        }
    }
}
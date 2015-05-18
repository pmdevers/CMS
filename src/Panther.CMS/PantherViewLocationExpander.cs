using System.Collections.Generic;

using Microsoft.AspNet.Mvc.Razor;

namespace Panther.CMS
{
    public class PantherViewLocationExpander : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            var locations = new List<string>(viewLocations);

            locations.Add("/Views/Templates/{0}.cshtml");

            return locations;
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {


            //throw new NotImplementedException();
        }
    }
}
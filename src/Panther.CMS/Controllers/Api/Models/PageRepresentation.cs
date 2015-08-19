using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Panther.Hal;

namespace Panther.CMS.Controllers.Api.Models
{
    public class PageRepresentation : Resource
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        protected override void CreateHypermedia()
        {
            Links.Add(LinkTemplates.GetPage());
        }
    }
}

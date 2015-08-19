using Panther.Hal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Panther.CMS.Controllers.Api.Models
{
    public class SiteRepresentation : Resource
    {
        public SiteRepresentation()
        {
            this.Rel = "site";
            this.Href = "/api/site";
        }

        public string SiteName { get; set; }
        public string Culture { get; set; }
        public Dictionary<string, string> Properties { get; set; }
        public List<string> Scripts { get; set; }
        public List<string> StyleSheet { get; set; }

        protected override void CreateHypermedia()
        {
            Links.Add(LinkTemplates.GetSite());
            Links.Add(LinkTemplates.GetPages());
            base.CreateHypermedia();
        }
    }
}

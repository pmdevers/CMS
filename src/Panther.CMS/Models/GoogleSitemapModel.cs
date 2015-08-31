using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Panther.CMS.Models
{
    [XmlRoot("urlset", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
    public class GoogleSiteMap
    {
        public void Create(string loc, DateTime lastModified, string prioity, ChangeFrequency changeFrequency, IEnumerable<MapNodeLink> links = null )
        {
            var node = new MapNode
            {
                Loc = loc,
                Priority = prioity,
                ChangeFrequenty = changeFrequency.ToString().ToLowerInvariant(),
                LastModified = lastModified.ToString("yyyy-MM-ddThh:mm:sszzz"),
                Links = links?.ToList()
                
            };
            List.Add(node);
        }

        public string GetXMLString()
        {
            var settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = ("    "),
                Encoding = new UTF8Encoding(false),
                OmitXmlDeclaration = true
            };
            using (var str = new StringWriter())
            using (var writer = XmlWriter.Create(str, settings))
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "http://www.sitemaps.org/schemas/sitemap/0.9") });
                ns.Add("xhtml", "http://www.w3.org/1999/xhtml");
                //ns.Add(_newsSiteMapPrefix, _newsSiteMapSchema);
                var xser = new XmlSerializer(typeof(GoogleSiteMap));
                xser.Serialize(writer, this, ns);
                return str.ToString();
            }
        }

        private List<MapNode> list = null;

        [XmlElement("url")]
        public List<MapNode> List
        {
            get
            {
                if (list == null)
                {
                    list = new List<MapNode>();
                }
                return list;
            }
        }

        public class MapNode
        {
            public MapNode()
            {
                Links = new List<MapNodeLink>();
            }

            [XmlElement("loc")]
            public string Loc { get; set; }

            [XmlElement("priority")]
            public string Priority { get; set; }

            [XmlElement("lastmod")]
            public string LastModified { get; set; }

            [XmlElement("changefreq")]
            public string ChangeFrequenty { get; set; }

            [XmlElement("link", Namespace = "http://www.w3.org/1999/xhtml")]
            public List<MapNodeLink> Links { get; set; } 
        }

        public class MapNodeLink
        {
            //<xhtml:link rel="alternate" hreflang="en" href="http://mysite.it/en" />
            [XmlAttribute("rel")]
            public string Rel { get; set; }
            [XmlAttribute("hreflang")]
            public string HrefLang { get; set; }
            [XmlAttribute("href")]
            public string Href { get; set; }
        }
    }

    public enum ChangeFrequency
    {
        Always,
        Hourly,
        Daily,
        Weekly,
        Monthly,
        Yearly,
        Never
    }
}

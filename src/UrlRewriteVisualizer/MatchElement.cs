using System.Xml.Linq;

namespace UrlRewriteVisualizer
{
    public class MatchElement
    {
        public MatchElement(XElement element)
        {
            Url = (string)element.Attribute("url");
            IgnoreCase = (bool?)element.Attribute("ignoreCase") ?? false;
            Negate = (bool?)element.Attribute("negate") ?? false;
        }

        public string Url { get; set; }

        public bool IgnoreCase { get; set; }

        public bool Negate { get; set; }
    }
}
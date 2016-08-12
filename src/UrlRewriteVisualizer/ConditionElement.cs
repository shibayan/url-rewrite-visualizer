using System.Xml.Linq;

namespace UrlRewriteVisualizer
{
    public class ConditionElement
    {
        public ConditionElement(XElement element)
        {
            Input = (string)element.Attribute("input");
            Pattern = (string)element.Attribute("pattern");
            IgnoreCase = (bool?)element.Attribute("ignoreCase") ?? false;
            Negate = (bool)element.Attribute("negate");
        }

        public string Input { get; set; }
        public string Pattern { get; set; }
        public bool IgnoreCase { get; set; }
        public bool Negate { get; set; }
    }
}
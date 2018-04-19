using System.Xml.Linq;

using UrlRewriteVisualizer.Internal;

namespace UrlRewriteVisualizer
{
    public class ActionElement
    {
        public ActionElement(XElement element)
        {
            Type = EnumHelper.ParseOrDefault<ActionType>((string)element.Attribute("type"));
            Url = (string)element.Attribute("url");
        }

        public ActionType Type { get; set; }

        public string Url { get; set; }
    }
}
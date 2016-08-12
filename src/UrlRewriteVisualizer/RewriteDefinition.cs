using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace UrlRewriteVisualizer
{
    public class RewriteDefinition
    {
        public IList<RuleElement> Rules { get; private set; }

        public static RewriteDefinition Load(string path)
        {
            var document = XDocument.Load(path);

            var definition = new RewriteDefinition
            {
                Rules = document.Descendants("rule").Select(x => new RuleElement(x)).ToArray()
            };

            return definition;
        }
    }
}

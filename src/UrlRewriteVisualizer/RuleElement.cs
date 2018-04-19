using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using UrlRewriteVisualizer.Internal;

namespace UrlRewriteVisualizer
{
    public class RuleElement : IEquatable<RuleElement>
    {
        internal RuleElement(XElement element)
        {
            Name = (string)element.Attribute("name");
            Enabled = (bool?)element.Attribute("enabled") ?? false;
            PatternSyntax = EnumHelper.ParseOrDefault((string)element.Attribute("patternSyntax"), PatternSyntax.ECMAScript);
            StopProcessing = (bool?)element.Attribute("stopProcessing") ?? false;

            Match = new MatchElement(element.Element("match"));
            Conditions = element.Element("conditions")?.Elements("add").Select(x => new ConditionElement(x)).ToArray();
            Action = new ActionElement(element.Element("action"));
        }

        public string Name { get; set; }

        public bool Enabled { get; set; }

        public PatternSyntax PatternSyntax { get; set; }

        public bool StopProcessing { get; set; }

        public MatchElement Match { get; set; }

        public IList<ConditionElement> Conditions { get; set; }

        public ActionElement Action { get; set; }

        public bool Equals(RuleElement other)
        {
            if (other == null)
            {
                return false;
            }

            return Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as RuleElement);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
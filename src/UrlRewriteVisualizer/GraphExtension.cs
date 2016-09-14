using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Media;

using Microsoft.VisualStudio.GraphModel;

namespace UrlRewriteVisualizer
{
    public static class GraphExtension
    {
        public static Graph Build(this RewriteDefinition definition)
        {
            return Build(definition, new GraphOptions());
        }

        public static Graph Build(this RewriteDefinition definition, GraphOptions options)
        {
            var graph = new Graph();

            var properties = graph.DocumentSchema.Properties;

            var background = properties.AddNewProperty("Background", typeof(Brush));

            // Analyze Dependency
            var list = definition.Rules
                                 .Select(x => new Fare.Xeger(x.Match.Url))
                                 .Select(x =>
                                 {
                                     var dependency = new List<RuleElement>();

                                     FindDependency(x.Generate(), definition, dependency);

                                     return dependency;
                                 });

            // Create Root Node
            foreach (var item in definition.Rules)
            {
                var node = graph.Nodes.GetOrCreate(item.Name);

                node.Label = item.Name;
            }

            // Create Node Links
            foreach (var dependency in list.Where(x => x.Count > 1))
            {
                var prevItem = dependency[0];

                foreach (var item in dependency.Skip(1))
                {
                    var prevNode = graph.Nodes.GetOrCreate(prevItem.Name);
                    var node = graph.Nodes.GetOrCreate(item.Name);

                    graph.Links.GetOrCreate(prevNode, node);

                    prevItem = item;
                }
            }

            // Colorize
            var incomingBrush = new SolidColorBrush(options.IncomingColor);
            var outgoingBrush = new SolidColorBrush(options.OutgoingColor);

            foreach (var node in graph.Nodes.Where(x => x.IncomingLinkCount == 0 && x.OutgoingLinkCount > 0))
            {
                node[background] = outgoingBrush;
            }

            foreach (var node in graph.Nodes.Where(x => x.IncomingLinkCount > 0 && x.OutgoingLinkCount == 0))
            {
                node[background] = incomingBrush;
            }

            return graph;
        }

        private static void FindDependency(string input, RewriteDefinition definition, List<RuleElement> dependency)
        {
            foreach (var item in definition.Rules)
            {
                var match = Regex.Match(input, item.Match.Url);

                if (!match.Success)
                {
                    continue;
                }

                dependency.Add(item);

                if (item.Action.Type == ActionType.CustomResponse || item.Action.Type == ActionType.AbortRequest)
                {
                    return;
                }

                if (item.Action.Type == ActionType.Rewrite || item.StopProcessing)
                {
                    return;
                }

                var url = item.Action.Url;

                for (int i = 0; i < match.Groups.Count; i++)
                {
                    url = url.Replace("{R:" + i + "}", match.Groups[i].Value);
                }

                if (url.StartsWith("http"))
                {
                    url = url.Substring(url.IndexOf('/', 9) + 1);
                }

                FindDependency(url, definition, dependency);

                return;
            }
        }
    }
}

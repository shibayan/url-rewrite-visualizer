using System.Windows.Media;

using UrlRewriteVisualizer;

namespace VisualizerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var definition = RewriteDefinition.Load(@"C:\Users\shibayan\Documents\Web.config");

            var graph = definition.Build(new GraphOptions
            {
                IncomingColor = Color.FromRgb(0, 128, 0),
                OutgoingColor = Color.FromRgb(0, 0, 128)
            });

            graph.Save(@"C:\Users\shibayan\Documents\Rewrite.dgml");
        }
    }
}

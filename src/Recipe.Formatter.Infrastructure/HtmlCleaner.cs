using System.Linq;
using HtmlAgilityPack;
using Recipe.Formatter.Interfaces;

namespace Recipe.Formatter.Infrastructure
{
    public class HtmlCleaner : IHtmlCleaner
    {
        public string Clean(string html)
        {
            var doc = new HtmlDocument();

            doc
                .LoadHtml(html);

            // Remove non-recipe elements
            var tagsToRemove = new[]
            {
                "script",
                "style",
                "nav",
                "header",
                "footer",
                "aside",
                "iframe",
                "noscript",
                "path",
                "link",
                "meta",
                "defs",
                "svg",
                "g"
            };

            foreach (var tag in tagsToRemove)
            {
                var nodes =
                    doc
                        .DocumentNode
                        .SelectNodes($"//{tag}");

                foreach (var node in nodes?.ToList() ?? [])
                    node?
                        .Remove();
            }

            // Remove by class/id patterns - be more specific
            var unwantedSelectors = new[]
            {
                "//*[contains(@class, 'advertisement')]",
                "//*[contains(@class, 'ad-')]",
                "//*[contains(@class, 'sidebar')]",
                "//*[contains(@class, 'comment')]",
                "//*[contains(@class, 'social')]",
                "//*[contains(@class, 'related')]",
                "//*[contains(@class, 'navigation')]"
            };

            foreach (var selector  in unwantedSelectors)
            {
                var nodes =
                    doc
                        .DocumentNode
                        .SelectNodes(selector);

                foreach (var node in nodes?.ToList() ?? [])
                    node?
                        .Remove();
            }

            // Remove all the attributes from the nodes - in theory this shouldn't affect the data
            foreach (var node in doc.DocumentNode.SelectNodes("//*"))
            {
                node
                    .Attributes
                    .RemoveAll();
            }

            var result =
                doc
                    .DocumentNode
                    .OuterHtml;

            result =
                result
                    .Replace("\n", string.Empty)
                    .Replace("\r", string.Empty)
                    .Replace("\t", string.Empty);

            return result;
        }
    }
}
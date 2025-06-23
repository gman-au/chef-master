using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Recipe.Formatter.Infrastructure.Extensions;

namespace Recipe.Formatter.Infrastructure
{
    public class JsonStripper : IJsonStripper
    {
        private const string ScriptRegexPattern = "\\<script type=\"application\\/(ld\\+json)\".*?\\<\\/script>";
        private const string JsonRegexPattern = "\\{.*\\}";
        private const string RecipeRegexPattern = "\\\"@type\\\"[ ]?:[ ]?\\\"recipe\\\"";

        public async Task<string> StripFromHtmlAsync(string html)
        {
            var allMatches = new List<string>();

            var scriptMatches =
                new Regex(ScriptRegexPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline)
                    .Matches(html);

            if (scriptMatches.Any())
                foreach (var match in scriptMatches)
                {
                    var jsonMatch =
                        new Regex(JsonRegexPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline)
                            .Match(match.ToString());

                    if (jsonMatch.Success)
                    {
                        var jsonString =
                            jsonMatch
                                .Value
                                .Sanitise();

                        var recipeMatch =
                            new Regex(RecipeRegexPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline)
                                .Match(jsonString);

                        if (recipeMatch.Success)
                        {
                            var openIndex =
                                jsonString
                                    .Sanitise()
                                    .GetBackwardNestingOccurrence(recipeMatch.Index, '{', '}');

                            // .GetPrecedingOccurrence(recipeMatch.Index, '{');
                            if (openIndex.HasValue)
                            {
                                var closeIndex = jsonString.GetForwardNestingOccurrence(openIndex.Value, '{', '}');

                                if (closeIndex.HasValue)
                                {
                                    var clipped =
                                        jsonString
                                            .Substring(
                                                openIndex.Value,
                                                closeIndex.Value - openIndex.Value + 1);

                                    allMatches.Add(clipped.Sanitise());
                                }
                            }
                        }
                    }
                }

            if (allMatches.Any())
            {
                var longestEntryLength =
                    allMatches
                        .Max(o => o.Length);

                var longestEntry =
                    allMatches
                        .FirstOrDefault(o => o.Length == longestEntryLength);

                if (longestEntry != null)
                    return longestEntry;
            }

            throw new Exception("I couldn't find any recipe information in that site. I'm still learning.");
        }
    }
}
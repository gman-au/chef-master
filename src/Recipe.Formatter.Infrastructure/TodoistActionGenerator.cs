using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Recipe.Formatter.Interfaces;

namespace Recipe.Formatter.Infrastructure
{
    public class TodoistActionGenerator : ITodoistActionGenerator
    {
        public string Generate(
            string recipeName,
            IEnumerable<string> ingredients)
        {
            if (!(ingredients ?? []).Any())
                return null;

            var result = new StringBuilder();

            result
                .Append("todoist://")
                .Append("addtask?");

            result
                .Append($"content={WebUtility.UrlEncode(recipeName)}");

            return
                result
                    .ToString();
        }
    }
}
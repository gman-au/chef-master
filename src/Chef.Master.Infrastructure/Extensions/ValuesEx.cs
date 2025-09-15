using System.Linq;
using Schema.NET;

namespace Chef.Master.Infrastructure.Extensions
{
    public static class ValuesEx
    {
        public static string GetLongestValue(this IValues values)
        {
            var strings =
                values
                    .OfType<string>()
                    .ToList();

            if (!strings.Any()) return null;

            var longestEntryLength =
                strings
                    .Max(o => o.Length);

            var longestEntry =
                strings
                    .FirstOrDefault(o => o.Length == longestEntryLength);

            return longestEntry;
        }
    }
}
using System;
using System.Net;

namespace Recipe.Formatter.Infrastructure.Extensions
{
    public static class StringEx
    {
        public static string Sanitise(this string value)
        {
            return
                value
                    .Replace("\n", "")
                    .Replace("\t", "")
                    .Replace("  ", "");
        }

        public static int? GetPrecedingOccurrence(this string value, int currentIndex, char character)
        {
            var index = 0;
            var priorIndex = 0;

            while (index < currentIndex && index > -1)
            {
                priorIndex = index;
                index++;
                index = value.IndexOf(character, index);
            }

            return priorIndex;
        }

        public static int? GetForwardNestingOccurrence(this string value, int currentIndex, char nestOpen,
            char nestClose)
        {
            var index = currentIndex;
            var nesting = 0;

            var nextOpenIndex = 0;
            var nextCloseIndex = 0;

            while ((nextCloseIndex > -1 || nextOpenIndex > -1) && nesting >= 0)
            {
                index++;
                nextOpenIndex = value.IndexOf(nestOpen, index);
                nextCloseIndex = value.IndexOf(nestClose, index);

                if (nextCloseIndex > -1 && (nextCloseIndex < nextOpenIndex || nextOpenIndex == -1))
                {
                    index = nextCloseIndex;
                    nesting--;
                }

                if (nextOpenIndex > -1 && (nextOpenIndex < nextCloseIndex || nextCloseIndex == -1))
                {
                    index = nextOpenIndex;
                    nesting++;
                }
            }

            return index;
        }

        public static int? GetBackwardNestingOccurrence(this string value, int currentIndex, char nestOpen,
            char nestClose)
        {
            var reversed =
                value
                    .ReverseString();

            var newIndex = reversed.Length - currentIndex;

            var index =
                reversed
                    .GetForwardNestingOccurrence(newIndex, nestClose, nestOpen);

            var result = reversed.Length - index - 1;

            return result;
        }

        public static string ReverseString(this string value)
        {
            var array = value.ToCharArray();
            Array.Reverse(array);
            return new string(array);
        }

        public static string ToFormatted(this string value)
        {
            value = WebUtility.HtmlDecode(value);

            return value;
        }
    }
}
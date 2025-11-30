using System.Drawing;
using System.Text.RegularExpressions;

namespace Helpers;

public static partial class StringExtensions
{
    [GeneratedRegex(@"\s+")]
    private static partial Regex WhiteSpaceRegex();

    extension(string str)
    {
        public string NormalizeWhitespace() => WhiteSpaceRegex().Replace(str, " ");
        public string RemoveWhitespace() => WhiteSpaceRegex().Replace(str, string.Empty);
        public string RemoveFromString(params char[] chars)
        {
            foreach (char c in chars)
                str = str.Replace(c.ToString(), string.Empty);
            return str;
        }
        public string RemoveFromString(params string[] strings)
        {
            foreach (string s in strings)
                str = str.Replace(s, string.Empty);
            return str;
        }
        public IEnumerable<int> ExtractNumbers() => str.Where(char.IsNumber).Select(Convert.ToInt32);
    }

    extension(IEnumerable<char> chars)
    {
        public string ConcatChars() => string.Concat(chars);
    }

    extension(IEnumerable<int> ints)
    {
        public Point ToPoint() => new(ints.First(), ints.Last());
        public IEnumerable<int> Odd() => ints.Where(i => i % 2 != 0);
        public IEnumerable<int> Even() => ints.Where(i => i % 2 == 0);
        public long Product() => ints.Aggregate(1, (a, b) => a * b);
    }
}

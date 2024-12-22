using System.Drawing;
using System.Text.RegularExpressions;

namespace Helpers;

public static partial class StringExtensions
{
    [GeneratedRegex(@"\s+")]
    private static partial Regex WhiteSpaceRegex();
    public static string NormalizeWhitespace(this string str) => WhiteSpaceRegex().Replace(str, " ");
    public static string RemoveWhitespace(this string str) => WhiteSpaceRegex().Replace(str, string.Empty);
    public static string ConcatChars(this IEnumerable<char> chars) => string.Concat(chars);
    public static string RemoveFromString(this string str, params char[] chars)
    {
        foreach (char c in chars)
            str = str.Replace(c.ToString(), string.Empty);
        return str;
    }
    public static string RemoveFromString(this string str, params string[] strings)
    {
        foreach (string s in strings)
            str = str.Replace(s, string.Empty);
        return str;
    }
    public static Point ToPoint(this IEnumerable<int> ints) => new Point(ints.First(), ints.Last());
}
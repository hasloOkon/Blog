using System.Text;
using System.Text.RegularExpressions;

namespace Blog.Core.Utility
{
    public static class StringExtensions
    {
        public static string Slugify(this string text)
        {
            return text
                .ToLower()
                .RemoveAccents()
                .RemoveInvalidCharacters()
                .Trim()
                .ReplaceWhitespacesWithUnderscores();
        }

        private static string RemoveAccents(this string text)
        {
            return Encoding.UTF8.GetString(Encoding.GetEncoding("ISO-8859-8").GetBytes(text));
        }

        private static string ReplaceWhitespacesWithUnderscores(this string text)
        {
            return Regex.Replace(text, @"\s+", "_");
        }

        private static string RemoveInvalidCharacters(this string text)
        {
            return Regex.Replace(text, @"[^a-z0-9_\s]", string.Empty);
        }
    }
}

namespace Blog.Core.Utility
{
    public static class StringExtensions
    {
        public static string Slugify(this string text)
        {
            return text
                .ToLower()
                .Replace(' ', '_');
        }
    }
}

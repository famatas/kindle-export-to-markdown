using KindleExportToMarkdown.Interfaces;
using System.Text.RegularExpressions;

namespace KindleExportToMarkdown.Services
{
    public class FormatterService : IFormatterService
    {
        public bool ContainsSubTitle(string value)
        {
            return !string.IsNullOrEmpty(FormatSubTitle(value));
        }

        public string FormatNotePage(string value)
        {
            return GetRegexMatch(value, @"(-|>) ([A-Z])\w+ [0-9]+");
        }

        public string FormatSubTitle(string value)
        {
            // regex: - (:) (.*?) >
            var c = value.Replace("\r\n", string.Empty);
            c = c.Replace(System.Environment.NewLine, string.Empty);
            c = Regex.Replace(c, @"\s+", " ");
            var a = GetRegexMatch(c, @"(:) (.*?) >");
            var s = !string.IsNullOrEmpty(a) ? a.Substring(1, a.Length - 2).Trim() : "";
            return s;
        }

        public string GetRegexMatch(string value, string pattern)
        {
            return Regex.Match(value, pattern).Value;
        }

        public string RemoveExtraSpaces(string value)
        {
            var c = value.Replace("\r\n", string.Empty);
            c = c.Replace(Environment.NewLine, string.Empty);
            c = Regex.Replace(c, @"\s+", " ");
            return c;
        }
    }
}

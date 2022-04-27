using KindleExportToMarkdown.Interfaces;
using System.Text.RegularExpressions;

namespace KindleExportToMarkdown.Services
{
    public class FormatterService : IFormatterService
    {
        public bool ContainsSubTitle(string value)
        {
            return string.IsNullOrEmpty(FormatSubTitle(value));
        }

        public string FormatNotePage(string value)
        {
            return GetRegexMatch(value, @"(-|>) ([A-Z])\w+ [0-9]+");
        }

        public string FormatSubTitle(string value)
        {
            // regex: - (:) (.*?) >
            return GetRegexMatch(value, @"");
        }

        public string GetRegexMatch(string value, string pattern)
        {
            return Regex.Match(value, pattern).Value;
        }
    }
}

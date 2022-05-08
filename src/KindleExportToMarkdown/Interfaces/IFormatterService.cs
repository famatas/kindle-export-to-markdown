using KindleExportToMarkdown.Models;

namespace KindleExportToMarkdown.Interfaces
{
    public interface IFormatterService
    {
        string GetRegexMatch(string value, string pattern);
        string FormatNotePage(string value);
        string FormatSubTitle(string value);
        bool ContainsSubTitle(string value);

        string RemoveExtraSpaces(string value);
        string GetMarkdownCode(Book book);
    }
}

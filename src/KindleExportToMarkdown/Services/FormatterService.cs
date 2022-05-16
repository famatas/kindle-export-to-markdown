using KindleExportToMarkdown.Constants;
using KindleExportToMarkdown.Interfaces;
using KindleExportToMarkdown.Models;
using System.Text;
using System.Text.RegularExpressions;

namespace KindleExportToMarkdown.Services
{
    public class FormatterService : IFormatterService
    {
        private const string NotePageFormat = @"(-|>) ([A-Z])\w+ [0-9]+";
        private const string SubtitleFormat = @"(:) (.*?) >";

        public bool ContainsSubTitle(string value) => !string.IsNullOrEmpty(FormatSubTitle(value));

        public string FormatNotePage(string value) => GetRegexMatch(value, NotePageFormat);

        public string GetRegexMatch(string value, string pattern) => Regex.Match(value, pattern).Value;

        public string RemoveExtraSpaces(string value) => Regex.Replace(RemoveEmptyLines(value), @"\s+", " ");        

        public string FormatSubTitle(string value)
        {
            var newValue = Regex.Replace(RemoveEmptyLines(value), @"\s+", " ");
            var regexMatch = GetRegexMatch(newValue, SubtitleFormat);
            return !string.IsNullOrEmpty(regexMatch) ? regexMatch[1..^1].Trim() : "";
        }

        public string GetMarkdownCode(Book book)
        {
            StringBuilder strb = new StringBuilder();

            strb.Append(FormatTitle1(book.Title));
            strb.Append(FormatAuthor(book.Author));

            foreach (var chapter in book.Chapters)
            {           
                strb.Append(FormatTitle2(chapter.Title));

                foreach (var subChapter in chapter.Subchapters)
                {
                    strb.Append(FormatTitle3(subChapter.Title));

                    foreach (var highlight in subChapter.Highlights)
                        strb.Append(FormatHighlight(highlight.Content, highlight.Page));
                }
            }

            return strb.ToString();
        }

        private string RemoveEmptyLines(string value) => value.Replace($"\r\n", string.Empty).Replace(Environment.NewLine, string.Empty);

        private string? FormatElement(string label, string value) => label + value;

        private string? FormatTitle1(string title) => FormatElement(MarkdownLabels.Title, title) + $"\n\n";

        private string FormatTitle2(string title) => $"\n\n" + FormatElement(MarkdownLabels.Title2, title) + $"\n\n";

        private string FormatTitle3(string title) => FormatElement(MarkdownLabels.Title3, title) + $"\n\n\n";

        private string FormatAuthor(string author) => FormatElement(MarkdownLabels.Note, author) + $"\n\n";

        private string FormatHighlight(string highlight, string page) => "- " + highlight + " " + MarkdownLabels.Bold + page + MarkdownLabels.Bold + $"\n\n";
    }
}

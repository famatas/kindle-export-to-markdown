using KindleExportToMarkdown.Interfaces;
using KindleExportToMarkdown.Models;
using System.Text;
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

        public string GetMarkdownCode(Book book)
        {
            StringBuilder strb = new StringBuilder();

            strb.Append("# ").Append(book.Title).Append("\n\n");
            strb.Append("> ").Append(book.Author).Append("\n\n");

            foreach (var chapter in book.Chapters)
            {
                strb.Append("\n\n").Append("## ").Append(chapter.Title).Append("\n\n");

                foreach (var subChapter in chapter.Subchapters)
                {
                    strb.Append("### ").Append(subChapter.Title).Append("\n\n\n");

                    foreach (var highlight in subChapter.Highlights)
                    {
                        strb.Append("- ").Append(highlight.Content).Append(" **").Append(highlight.Page).Append("**").Append("\n\n");
                    }
                }
            }

            return strb.ToString();
        }
    }
}

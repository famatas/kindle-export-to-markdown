using HtmlAgilityPack;
using KindleExportToMarkdown.Interfaces;

namespace KindleExportToMarkdown.Services
{
    public class HTMLScrapperService : IScrapperService
    {
        public string GetTitle(HtmlDocument document)
        {
            var htmlTitle = document.DocumentNode.QuerySelector("div.bookTitle");
            return htmlTitle.InnerText.Trim();
        }

        public HtmlDocument GetDocument(string document)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(document);
            return htmlDoc;
        }

        public string GetAuthor(HtmlDocument document)
        {
            var author = document.DocumentNode.QuerySelector("div.authors");
            return author.InnerText.Trim();
        }

        public string GetSectionTitle(HtmlDocument document)
        {
            var author = document.DocumentNode.QuerySelector("div.sectionHeading");
            return author.InnerText.Trim();
        }
    }
}

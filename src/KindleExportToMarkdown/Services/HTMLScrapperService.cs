using HtmlAgilityPack;
using KindleExportToMarkdown.Interfaces;

namespace KindleExportToMarkdown.Services
{
    public class HTMLScrapperService : IScrapperService
    {
        public string GetTitle(HtmlDocument document) => GetNodeValue(document, "div.bookTitle");

        public HtmlDocument GetDocument(string document)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(document);
            return htmlDoc;
        }

        public string GetAuthor(HtmlDocument document) => GetNodeValue(document, "div.authors");

        public string GetSectionTitle(HtmlDocument document) => GetNodeValue(document, "div.sectionHeading");
        
        public void RemoveSectionTitle(HtmlDocument document) => RemoveElement(document, "div.sectionHeading");

        public bool IsLastSection(HtmlDocument document) => ContainsElement(document, "div.sectionHeading");

        public string GetSubsectionTitle(HtmlDocument document) => throw new NotImplementedException();

        public string GetNoteText(HtmlDocument document) => GetNodeValue(document, "div.noteText");

        public string GetNotePage(HtmlDocument document) => throw new NotImplementedException();        

        public string GetNoteHeading(HtmlDocument document) => GetNodeValue(document, "div.noteHeading");

        private void RemoveElement(HtmlDocument document, string selector)
        {
            var node = document.DocumentNode.QuerySelector(selector);
            node.Remove();
        }

        private bool ContainsElement(HtmlDocument document, string selector)
        {
            var value = document.DocumentNode.QuerySelector(selector);
            return value == null;
        }
        private string GetNodeValue(HtmlDocument document, string selector)
        {
            var value = document.DocumentNode.QuerySelector(selector);
            return value.InnerText.Trim();
        }
    }
}

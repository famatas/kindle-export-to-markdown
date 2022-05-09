using HtmlAgilityPack;
using KindleExportToMarkdown.Interfaces;

namespace KindleExportToMarkdown.Services
{
    public class HTMLScrapperService : IScrapperService
    {
        public string GetTitle(HtmlDocument document) => GetNodeValue(document, "div.bookTitle");        

        public string GetAuthor(HtmlDocument document) => GetNodeValue(document, "div.authors");

        public string GetSectionTitle(HtmlDocument document, int index) => GetNodeValue(document, $"div.sectionHeading-{index}");
        
        public void RemoveNoteHeading(HtmlDocument document, int index) => RemoveElement(document, $"div.noteHeading-{index}");

        public HtmlNode GetNoteHeadingNode(HtmlDocument document, int index) => GetNode(document, $"div.sectionHeading-{index}");

        public void RemoveNoteText(HtmlDocument document, int index) => RemoveElement(document, $"div.noteText-{index}");

        public string GetNoteText(HtmlDocument document, int index) => GetNodeValue(document, $"div.noteText-{index}");   

        public string GetNoteHeading(HtmlDocument document, int index) => GetNodeValue(document, $"div.noteHeading-{index}");

        private HtmlNode GetNode(HtmlDocument document, string selector) => document.DocumentNode.QuerySelector(selector);

        public HtmlDocument GetDocument(string document)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(document);
            return htmlDoc;
        }

        private void RemoveElement(HtmlDocument document, string selector)
        {
            var node = document.DocumentNode.QuerySelector(selector);
            node.Remove();
        }

        // TODO: Review best null practices
        private string GetNodeValue(HtmlDocument document, string selector)
        {
            var node = GetNode(document, selector);
            if (node != null) return node.InnerText.Trim();
            else return null;
        }
    }
}

using HtmlAgilityPack;
using KindleExportToMarkdown.Constants;
using KindleExportToMarkdown.Interfaces;

namespace KindleExportToMarkdown.Services
{
    public class HTMLScrapperService : IScrapperService
    {
        public string GetTitle(HtmlDocument document) => GetNodeValue(document, HTMLSelectors.BookTitle);        

        public string GetAuthor(HtmlDocument document) => GetNodeValue(document, HTMLSelectors.Authors);

        public string GetSectionTitle(HtmlDocument document, int index) => GetNodeValue(document, $"{HTMLSelectors.ChapterTitle}-{index}");
        
        public void RemoveNoteHeading(HtmlDocument document, int index) => RemoveElement(document, $"{HTMLSelectors.Note}-{index}");

        public HtmlNode GetNoteHeadingNode(HtmlDocument document, int index) => GetNode(document, $"{HTMLSelectors.ChapterTitle}-{index}");

        public void RemoveNoteText(HtmlDocument document, int index) => RemoveElement(document, $"{HTMLSelectors.HighlightText}-{index}");

        public string GetNoteText(HtmlDocument document, int index) => GetNodeValue(document, $"{HTMLSelectors.HighlightText}-{index}");   

        public string GetNoteHeading(HtmlDocument document, int index) => GetNodeValue(document, $"{HTMLSelectors.Note}-{index}");

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

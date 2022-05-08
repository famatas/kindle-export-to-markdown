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

        public void RemoveNoteHeading(HtmlDocument document) => RemoveElement(document, "div.noteHeading");
        public void RemoveNoteText(HtmlDocument document) => RemoveElement(document, "div.noteText");

        public bool IsLastSection(HtmlDocument document) => ContainsElement(document, "div.sectionHeading");

        public string GetNoteText(HtmlDocument document) => GetNodeValue(document, "div.noteText");   

        public string GetNoteHeading(HtmlDocument document) => GetNodeValue(document, "div.noteHeading");

        public bool IsNextElementNewChapter(HtmlDocument document)
        {
            var emptyLine = true;
            //var node = GetNextElement(document, "div.noteText"); // ACA ESTA EL ERROR, VER COMO MANEJAR SI VIENE VACIO SIN SALTAR AL SIGUIENTE ELEMENTO
            var node = GetNode(document, "div");
            while (emptyLine)
            {
                var a = node.OuterHtml.Replace("\r\n", string.Empty).Trim();
                if (!String.IsNullOrEmpty(a))
                {
                    emptyLine = false;
                    return node.OuterHtml.Contains("sectionHeading");
                }

                node = node.NextSiblingElement();
            }

            return false;
        }

        private HtmlNode GetNextElement(HtmlDocument document, string selector)
        {
            var node = document.DocumentNode.QuerySelector(selector);
            return node.NextSibling;
        }

        private void RemoveElement(HtmlDocument document, string selector)
        {
            var node = document.DocumentNode.QuerySelector(selector);
            node.Remove();
        }

        private bool ContainsElement(HtmlDocument document, string selector)
        {
            return GetNode(document, selector) == null;
        }
        private string GetNodeValue(HtmlDocument document, string selector)
        {
            return GetNode(document, selector).InnerText.Trim();
        }

        private HtmlNode GetNode(HtmlDocument document, string selector)
        {
            var node = document.DocumentNode.QuerySelector(selector);
            return node;
        }

        public string GetSectionTitle(HtmlDocument document, int index)
        {
            throw new NotImplementedException();
        }

        public string GetNoteText(HtmlDocument document, int index)
        {
            throw new NotImplementedException();
        }

        public string GetNoteHeading(HtmlDocument document, int index)
        {
            throw new NotImplementedException();
        }

        public HtmlNode GetNoteHeadingNode(HtmlDocument document, int index)
        {
            throw new NotImplementedException();
        }
    }
}

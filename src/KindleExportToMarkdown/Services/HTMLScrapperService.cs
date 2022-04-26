using HtmlAgilityPack;
using KindleExportToMarkdown.Interfaces;

namespace KindleExportToMarkdown.Services
{
    public class HTMLScrapperService : IScrapperService
    {
        public string GetTitle(HtmlDocument document)
        {
            return this.GetNodeValue(document, "div.bookTitle");
        }

        public HtmlDocument GetDocument(string document)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(document);
            return htmlDoc;
        }

        public string GetAuthor(HtmlDocument document)
        {
            return this.GetNodeValue(document, "div.authors");
        }

        public string GetSectionTitle(HtmlDocument document)
        {
            return this.GetNodeValue(document, "div.sectionHeading");
        }

        private string GetNodeValue(HtmlDocument document, string selector)
        {
            var value = document.DocumentNode.QuerySelector(selector);
            return value.InnerText.Trim();
        }
        public void RemoveSectionTitle(HtmlDocument document)
        {
            this.RemoveElement(document, "div.sectionHeading");
        }

        public bool isLastSection(HtmlDocument document)
        {
            var value = document.DocumentNode.QuerySelector("div.sectionHeading");
            return value == null;
        }

        public string GetSubsectionTitle(HtmlDocument document)
        {
            var noteHeading = this.GetNoteHeading(document);
            // regex: - (:) (.*?) >
            throw new NotImplementedException();
        }

        public string GetNoteText(HtmlDocument document)
        {
            return this.GetNodeValue(document, "div.noteText");
        }

        public string GetNotePage(HtmlDocument document)
        {
            var noteHeading = this.GetNoteHeading(document);
            // regex: (-|>) ([A-Z])\w+ [0-9\(\)]+

            /*Ejemplos
            Highlight(< span class="highlight_yellow">yellow</span>) - Page 634435

            Highlight(<span class="highlight_yellow">yellow</span>) - 1: The
                   Surprising Power of Atomic Habits > Page 15*/

            throw new NotImplementedException();
        }

        private void RemoveElement(HtmlDocument document, string selector)
        {
            var node = document.DocumentNode.QuerySelector(selector);
            node.Remove();
        }

        private string GetNoteHeading(HtmlDocument document)
        {
            return this.GetNodeValue(document, "div.noteHeading");
        }
    }
}

using HtmlAgilityPack;

namespace KindleExportToMarkdown.Interfaces
{
    public interface IScrapperService
    {
        HtmlDocument GetDocument(string document);
        string GetTitle(HtmlDocument document);
        string GetAuthor(HtmlDocument document);        
        string GetSectionTitle(HtmlDocument document, int index);        
        string GetNoteText(HtmlDocument document, int index);        
        string GetNoteHeading(HtmlDocument document, int index);
        HtmlNode GetNoteHeadingNode(HtmlDocument document, int index);
        void RemoveNoteHeading(HtmlDocument document, int index);
        void RemoveNoteText(HtmlDocument document, int index);
    }
}

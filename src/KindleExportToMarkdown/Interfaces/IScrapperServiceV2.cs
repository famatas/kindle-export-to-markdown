using HtmlAgilityPack;

namespace KindleExportToMarkdown.Interfaces
{
    public interface IScrapperServiceV2
    {
        HtmlDocument GetDocument(string document);
        string GetTitle(HtmlDocument document);
        string GetAuthor(HtmlDocument document);        
        string GetSectionTitle(HtmlDocument document, int index);        
        string GetNoteText(HtmlDocument document, int index);        
        string GetNoteHeading(HtmlDocument document, int index);
        HtmlNode GetNoteHeadingNode(HtmlDocument document, int index);
        void RemoveSectionTitle(HtmlDocument document);
        void RemoveNoteHeading(HtmlDocument document, int index);
        bool IsLastSection(HtmlDocument document);
        bool IsNextElementNewChapter(HtmlDocument document);
        void RemoveNoteText(HtmlDocument document, int index);
    }
}

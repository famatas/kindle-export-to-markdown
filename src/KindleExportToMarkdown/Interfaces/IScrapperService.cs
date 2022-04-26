using HtmlAgilityPack;

namespace KindleExportToMarkdown.Interfaces
{
    public interface IScrapperService
    {
        HtmlDocument GetDocument(string document);
        string GetTitle(HtmlDocument document);
        string GetAuthor(HtmlDocument document);        
        string GetSectionTitle(HtmlDocument document);
        string GetSubsectionTitle(HtmlDocument document);
        string GetNoteText(HtmlDocument document);
        string GetNotePage(HtmlDocument document);
        void RemoveSectionTitle(HtmlDocument document);
        bool isLastSection(HtmlDocument document);
    }
}

using HtmlAgilityPack;

namespace KindleExportToMarkdown.Interfaces
{
    public interface IScrapperService
    {
        string GetTitle(HtmlDocument document);
        string GetAuthor(HtmlDocument document);
        HtmlDocument GetDocument(string document);
    }
}

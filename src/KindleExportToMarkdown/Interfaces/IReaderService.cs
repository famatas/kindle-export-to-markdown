namespace KindleExportToMarkdown.Interfaces
{
    public interface IReaderService
    {
        Task<string> ReadContent(IFormFile file);
    }
}

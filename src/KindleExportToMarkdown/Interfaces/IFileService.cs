using KindleExportToMarkdown.Models;

namespace KindleExportToMarkdown.Interfaces
{
    public interface IFileService
    {
        Task<string> ReadContent(IFormFile file);

        bool isValidFile(IFormFile file);

        Task<Document> UpdateClasses(IFormFile file);
    }
}

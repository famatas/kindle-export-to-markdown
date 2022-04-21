using KindleExportToMarkdown.Interfaces;
using System.Text;

namespace KindleExportToMarkdown.Services
{
    public class ReaderFileService : IReaderService
    {
        public async Task<string> ReadContent(IFormFile file)
        {
            var result = new StringBuilder();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                {
                    result.AppendLine(await reader.ReadLineAsync());
                }

            }

            return result.ToString();
        }
    }
}

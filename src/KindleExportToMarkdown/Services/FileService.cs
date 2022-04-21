using KindleExportToMarkdown.Interfaces;
using System.Text;

namespace KindleExportToMarkdown.Services
{
    public class FileService : IFileService
    {
        private string[] permittedExtensions = { ".txt", ".pdf" };

        public bool isValidFile(IFormFile file)
        {
            return (!isEmpty(file) && isValidFormat(file));
        }

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

        private bool isEmpty(IFormFile file)
        {
            return file.Length == 0;
        }

        private bool isValidFormat(IFormFile file)
        {
            var fileExt = Path.GetExtension(file.FileName.Substring(1));
            return permittedExtensions.Contains(fileExt);
        }
    }
}

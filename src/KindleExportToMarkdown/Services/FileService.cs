using KindleExportToMarkdown.Interfaces;
using KindleExportToMarkdown.Models;
using System.Text;

namespace KindleExportToMarkdown.Services
{
    public class FileService : IFileService
    {
        private string[] permittedExtensions = { ".html", ".txt" };

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

        public async Task<Document> UpdateClasses(IFormFile file)
        {
            var content = await ReadContent(file);
            var index = 0;
            var result = new StringBuilder();            
            using (var reader = new StringReader(content))
            {
                for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
                {
                    if (line.Contains("class=\"noteHeading\"")) line = line.Replace("class=\"noteHeading\"", $"class=\"noteHeading-{index}\"");
                    else if (line.Contains("class=\"noteText\"")) line = line.Replace("class=\"noteText\"", $"class=\"noteText-{index}\"");
                    else if (line.Contains("class=\"sectionHeading\"")) 
                    {
                        index++;
                        line = line.Replace("class=\"sectionHeading\"", $"class=\"sectionHeading-{index}\""); 
                    }
                    
                    result.Append(line.Clone());
                }
            }

            return new Document()
            {
                Content = result.ToString(),
                Size = index
            };
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

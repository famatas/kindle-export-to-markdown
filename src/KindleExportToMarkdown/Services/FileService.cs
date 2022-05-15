
using Aspose.Html.Converters;
using Aspose.Html.Saving;
using KindleExportToMarkdown.Constants;
using KindleExportToMarkdown.Interfaces;
using KindleExportToMarkdown.Models;
using System.Text;
using System.Text.RegularExpressions;

namespace KindleExportToMarkdown.Services
{
    public class FileService : IFileService
    {
        private string[] permittedExtensions = { ".html", ".txt" };

        public bool isValidFile(IFormFile file) => (!isEmpty(file) && isValidFormat(file));

        public async Task<string> ReadContent(IFormFile file)
        {
            var result = new StringBuilder();
            using (var reader = new StreamReader(file.OpenReadStream()))
                while (reader.Peek() >= 0) result.AppendLine(await reader.ReadLineAsync());

            return result.ToString();
        }

        public async Task<Document> UpdateClasses(IFormFile file)
        {
            try
            {
                var content = await ReadContent(file);
                var indexSection = 0;
                var result = new StringBuilder();
                using (var reader = new StringReader(content))
                {
                    for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
                    {
                        if (line.Contains(CssClasses.Note)) line = ReplacePattern(CssClasses.Note, indexSection, line);
                        if (line.Contains(CssClasses.HighlightText)) line = ReplacePattern(CssClasses.HighlightText, indexSection, line);
                        if (line.Contains(CssClasses.ChapterTitle))
                        {
                            indexSection++;
                            line = ReplacePattern(CssClasses.ChapterTitle, indexSection, line);
                        }

                        result.Append(line.Clone());
                    }
                }

                return new Document()
                {
                    Content = result.ToString(),
                    Size = indexSection
                };
            } catch (Exception ex)
            {
                Console.WriteLine(ex); // HANDLE EXCEPTION
                return null;
            }            
        }

        private string ReplacePattern(string cssClass, int indexSection, string line)
        {
            var pattern = $"(\\\")$";
            return line.Replace(cssClass, Regex.Replace(cssClass, pattern, $"-{ indexSection}\""));
        }

        private bool isEmpty(IFormFile file) => file.Length == 0;

        private bool isValidFormat(IFormFile file)
        {
            var fileExt = Path.GetExtension(file.FileName.Substring(1));
            return permittedExtensions.Contains(fileExt);
        }

        public string GetPdfFile(string text)
        {
            string OutputDir = "temp";
            string sourcePath = Path.Combine(OutputDir, "document.md");
            File.WriteAllText(sourcePath, text);
            
            string savePath = Path.Combine(OutputDir, "document-output.pdf");

            // Convert Markdown to HTML document
            using var document = Converter.ConvertMarkdown(sourcePath);

            // Convert HTML document to PDF image file format
            Converter.ConvertHTML(document, new PdfSaveOptions(), savePath);

            return document.TextContent;
        }
    }
}

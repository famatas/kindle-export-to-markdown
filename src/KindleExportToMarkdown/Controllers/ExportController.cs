using KindleExportToMarkdown.Interfaces;
using KindleExportToMarkdown.Models;
using Microsoft.AspNetCore.Mvc;

namespace KindleExportToMarkdown.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExportController : ControllerBase
    {
        private IFileService fileService;
        private IScrapperService scrapperService;
        private IFormatterService formatterService;

        public ExportController(IFileService fileService, IScrapperService scrapperService, IFormatterService formatterService)
        {
            this.fileService = fileService;
            this.scrapperService = scrapperService;
            this.formatterService = formatterService;
        }

        [HttpPost(Name = "ExportToMarkdown")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ExportToMarkdown(IFormFile file)
        {
            if (this.fileService.isValidFile(file))
            {
                var content = await this.fileService.ReadContent(file);
                var document = this.scrapperService.GetDocument(content);

                var book = new Book();
                book.Title = this.scrapperService.GetTitle(document);
                book.Author = this.scrapperService.GetAuthor(document);

                List<Chapter> chapters = new List<Chapter>();

                var nextChapter = true;

                while(nextChapter)
                {
                    Chapter chapter = new Chapter();
                    chapter.Title = this.scrapperService.GetSectionTitle(document);
                    this.scrapperService.RemoveSectionTitle(document);

                    // Tengo 2 objetos noteHeading y noteText

                    var noteHeading = this.scrapperService.GetNoteHeading(document);

                    // Cuando cambie de subtitulo, tengo que tambien controlar el titulo para ver si no se fue de capitulo

                    chapters.Add(chapter);

                    var isLastChapter = this.scrapperService.IsLastSection(document);
                    nextChapter = (isLastChapter != true); 
                }

                book.Chapters = chapters;

                return Ok(book);
            }
            return BadRequest();
        }

        [HttpGet(Name = "test")]
        public async Task<IActionResult> Test()
        {

            return Ok();
        }
    }
}

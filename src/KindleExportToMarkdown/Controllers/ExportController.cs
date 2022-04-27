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

                var sameChapter = true;
                var sameSection = true;

                while (sameChapter)
                {
                    Chapter chapter = new Chapter();
                    chapter.Title = this.scrapperService.GetSectionTitle(document);
                    this.scrapperService.RemoveSectionTitle(document);

                    // Cuando cambie de subtitulo, tengo que tambien controlar el titulo para ver si no se fue de capitulo 
                    while(sameSection) // Mientras estemos en el mismo subtitulo
                    {
                        List<Subchapter> subChapters = new List<Subchapter>();
                        List<Highlight> highlights = new List<Highlight>();

                        // Tengo 2 objetos noteHeading (pagina y subtitulo) y noteText
                        var noteHeading = this.scrapperService.GetNoteHeading(document);

                        var subTitle = formatterService.ContainsSubTitle(noteHeading) ? formatterService.FormatSubTitle(noteHeading) : "EMPTY" ;
                        Subchapter subChapter = new Subchapter();
                        subChapter.Title = subTitle;

                        var page = formatterService.FormatNotePage(noteHeading);
                        var text = scrapperService.GetNoteText(document);

                        scrapperService.RemoveNoteHeading(document);

                        noteHeading = this.scrapperService.GetNoteHeading(document);
                        var newSubtitile = formatterService.ContainsSubTitle(noteHeading) ? formatterService.FormatSubTitle(noteHeading) : "EMPTY";

                        if(subTitle.Equals(newSubtitile)) // Estamos en la misma section
                        {
                            Highlight highlight = new Highlight();
                            highlight.Page = page;
                            highlight.Content = text;

                            highlights.Add(highlight);
                        } else
                        {     
                            // Ya no estamos en la misma section, hay que controla si estamos en el mismo capitulo sino continuar.
                        }
                    }

                    chapters.Add(chapter);

                    var isLastChapter = this.scrapperService.IsLastSection(document);
                    sameChapter = (isLastChapter != true); 
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

using HtmlAgilityPack;
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
                var updatedContent = await fileService.UpdateClasses(file);



                /*var document = this.scrapperService.GetDocument(content);

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

                    List<Subchapter> subChapters = new List<Subchapter>();
                    List<Highlight> highlights = new List<Highlight>();
                    Subchapter subChapter = new Subchapter();

                    // Cuando cambie de subtitulo, tengo que tambien controlar el titulo para ver si no se fue de capitulo 
                    while (sameSection) // Mientras estemos en el mismo subtitulo
                    {
                        // Tengo 2 objetos noteHeading (pagina y subtitulo) y noteText
                        var noteHeading = this.scrapperService.GetNoteHeading(document);

                        var subTitle = formatterService.ContainsSubTitle(noteHeading) ? formatterService.FormatSubTitle(noteHeading) : "EMPTY" ;
                        
                        subChapter.Title = subTitle;

                        var page = formatterService.FormatNotePage(noteHeading);
                        var text = scrapperService.GetNoteText(document);

                        scrapperService.RemoveNoteHeading(document);
                        scrapperService.RemoveNoteText(document);                       


                        if (scrapperService.IsNextElementNewChapter(document)) // No es el mismo capitulo
                        {
                            List<Subchapter> scCopies = (from scc in subChapters
                                                         select new Subchapter
                                                         {
                                                             Title = scc.Title,
                                                             Highlights = scc.Highlights,
                                                         }).ToList();


                            chapter.Subchapters = scCopies;
                            break;
                        }
                        else // Mismo capitulo
                        {
                            noteHeading = this.scrapperService.GetNoteHeading(document);
                            var newSubtitile = formatterService.ContainsSubTitle(noteHeading) ? formatterService.FormatSubTitle(noteHeading) : "EMPTY";

                            Highlight highlight = new Highlight();
                            highlight.Page = page;
                            highlight.Content = text;

                            highlights.Add(highlight);

                            if (subTitle.Equals(newSubtitile)) // Estamos en la misma section
                            {
                                // Something
                            }
                            else
                            {
                                // Ya no estamos en la misma section, hay que controlar si estamos en el mismo capitulo sino continuar.
                                List<Highlight> copies = (from hc in highlights
                                                          select new Highlight
                                                          {
                                                              Page = hc.Page,
                                                              Content = hc.Content
                                                          }).ToList();

                                subChapter.Highlights = copies;

                                subChapters.Add(subChapter.Clone());

                                subChapter.Title = string.Empty;
                                subChapter.Highlights = new List<Highlight>();
                                highlights = new List<Highlight>();

                                if (scrapperService.IsNextElementNewChapter(document)) // No es el mismo capitulo
                                {
                                    List<Subchapter> scCopies = (from scc in subChapters
                                                                 select new Subchapter
                                                                 {
                                                                     Title = scc.Title,
                                                                     Highlights = scc.Highlights,
                                                                 }).ToList();


                                    chapter.Subchapters = scCopies;
                                    break;
                                }
                            }
                        }
                    }

                    chapters.Add(chapter);

                    var isLastChapter = this.scrapperService.IsLastSection(document);
                    sameChapter = (isLastChapter != true); 
                }

                book.Chapters = chapters;

                return Ok(book);*/
            }
            return BadRequest();
        }

        [HttpGet(Name = "test")]
        public async Task<IActionResult> Test()
        {
            var html =
        @"<!DOCTYPE html>
            <html>
            <body>
	            <h1>This is <b>bold</b> heading</h1>
	            <p>This is <u>underlined</u> paragraph</p>
	            <h2>This is <i>italic</i> heading</h2>
	            <h2>This is new heading</h2>
            </body>
            </html> ";

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var node = htmlDoc.DocumentNode.SelectSingleNode("//body/h1");

            HtmlNode sibling = node.NextSibling;

            while (sibling != null)
            {
                if (sibling.NodeType == HtmlNodeType.Element)
                    Console.WriteLine(sibling.OuterHtml.Contains("underlined"));

                sibling = sibling.NextSibling;
            }
            return Ok();
        }

        private void GetUpdatedHeadings()
        {

        }
    }
}



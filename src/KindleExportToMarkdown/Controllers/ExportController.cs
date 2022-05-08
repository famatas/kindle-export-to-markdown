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
        private IScrapperServiceV2 scrapperService;
        private IFormatterService formatterService;

        public ExportController(IFileService fileService, IScrapperServiceV2 scrapperService, IFormatterService formatterService)
        {
            this.fileService = fileService;
            this.scrapperService = scrapperService;
            this.formatterService = formatterService;
        }

        [HttpPost(Name = "ExportBookDefinition")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ExportToMarkdown(IFormFile file)
        {
            try
            {
                if (this.fileService.isValidFile(file))
                {
                    var updatedContent = await fileService.UpdateClasses(file);

                    var document = this.scrapperService.GetDocument(updatedContent.Content);

                    var book = new Book();
                    book.Title = this.scrapperService.GetTitle(document);
                    book.Author = this.scrapperService.GetAuthor(document);

                    List<Chapter> chapters = new List<Chapter>();

                    for (int i = 1; i < updatedContent.Size; i++)
                    {
                        Chapter chapter = new Chapter();
                        List<Subchapter> subChapters = new List<Subchapter>();
                        chapter.Title = this.scrapperService.GetSectionTitle(document, i);

                        // We can use the noteHeader as pivot
                        var noteHeading = this.scrapperService.GetNoteHeading(document, i);
                        var noteHeadingNode = this.scrapperService.GetNoteHeadingNode(document, i);

                        var subTitle = formatterService.ContainsSubTitle(noteHeading) ? formatterService.FormatSubTitle(noteHeading) : "EMPTY";

                        Subchapter subChapter = new Subchapter();
                        subChapter.Title = subTitle;

                        var page = formatterService.FormatNotePage(noteHeading);
                        var text = formatterService.RemoveExtraSpaces(scrapperService.GetNoteText(document, i));

                        List<Highlight> highlights = new List<Highlight>();

                        Highlight highlight = new Highlight()
                        {
                            Page = page,
                            Content = text
                        };

                        highlights.Add(highlight);

                        while (noteHeadingNode.NextSibling != null && noteHeading !=null)
                        {
                            noteHeadingNode = noteHeadingNode.NextSibling;

                            scrapperService.RemoveNoteHeading(document, i);
                            scrapperService.RemoveNoteText(document, i);

                            noteHeading = scrapperService.GetNoteHeading(document, i);

                            var newSubTitle = (noteHeading != null && formatterService.ContainsSubTitle(noteHeading)) ? formatterService.FormatSubTitle(noteHeading) : "EMPTY";

                            if (!newSubTitle.Equals(subTitle) || noteHeading == null) // Sub chapter changed
                            {
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

                                subTitle = newSubTitle;
                                subChapter.Title = subTitle;
                            }
                            
                            if (noteHeading != null)
                            {
                                Highlight newHighlight = new Highlight()
                                {
                                    Page = formatterService.FormatNotePage(noteHeading),
                                    Content = formatterService.RemoveExtraSpaces(scrapperService.GetNoteText(document, i))
                                };

                                highlights.Add(newHighlight);
                            }
                        }

                        List<Subchapter> scCopies = (from scc in subChapters
                                                     select new Subchapter
                                                     {
                                                         Title = scc.Title,
                                                         Highlights = scc.Highlights,
                                                     }).ToList();


                        chapter.Subchapters = scCopies;

                        chapters.Add(chapter);
                    }

                    book.Chapters = chapters;

                    return Ok(book);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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

        [HttpPost(Name = "GetBookMarkdown")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetBookMarkdown(Book book)
        {            
            return Ok(formatterService.GetMarkdownCode(book));
        } 
    }
}



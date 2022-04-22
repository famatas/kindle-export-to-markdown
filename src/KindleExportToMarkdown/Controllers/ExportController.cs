using KindleExportToMarkdown.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KindleExportToMarkdown.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExportController : ControllerBase
    {
        private IFileService fileService;
        private IScrapperService scrapperService;

        public ExportController(IFileService fileService, IScrapperService scrapperService)
        {
            this.fileService = fileService;
            this.scrapperService = scrapperService;
        }

        [HttpPost(Name = "ExportToMarkdown")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ExportToMarkdown(IFormFile file)
        {
            if (this.fileService.isValidFile(file))
            {
                var content = await this.fileService.ReadContent(file);
                var body = this.scrapperService.GetTitle(content);
                return Ok(body);

            }
            return BadRequest();
        }
    }
}

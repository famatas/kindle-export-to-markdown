using KindleExportToMarkdown.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KindleExportToMarkdown.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExportController : ControllerBase
    {
        private IReaderService readerService;

        public ExportController(IReaderService readerService)
        {
            this.readerService = readerService;
        }

        [HttpPost(Name = "ExportToMarkdown")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ExportToMarkdown(IFormFile file)
        {
            if (file.Length > 0)
            {
                var result = await this.readerService.ReadContent(file);
                return Ok(result);

            }
            return BadRequest();
        }
    }
}

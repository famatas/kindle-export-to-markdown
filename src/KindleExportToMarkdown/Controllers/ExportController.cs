using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KindleExportToMarkdown.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExportController : ControllerBase
    {
        [HttpGet(Name = "TestExportToMarkdown")]
        public async Task<int> TestExportToMarkdown()
        {
            return await Task.FromResult<int>(1);
        }

        [HttpGet(Name = "ExportToMarkdown")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ExportToMarkdown()
        {
            return Ok();
        }
    }
}

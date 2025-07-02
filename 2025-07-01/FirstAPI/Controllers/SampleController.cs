using FirstAPI.Models.DTOs;
using FirstAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        public FileProcessingService _processingService;
        public SampleController(FileProcessingService fileProcessingService)
        {
            _processingService = fileProcessingService;
        }
        [HttpGet]
        public string GetGreet()
        {
            return "Hello World";
        }
        
        [HttpPost("FromCsv")]
        public async Task<IActionResult> BulkInsertFromCsv([FromBody] CsvUploadDto input)
        {
            return Ok(await _processingService.ProcessData(input));
        }
    }
}

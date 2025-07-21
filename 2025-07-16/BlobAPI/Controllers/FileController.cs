using System.Threading.Tasks;
using BlobAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlobAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private BlobStorageService _blobStorageService;
        public FileController(BlobStorageService blobStorageService)
        {
            _blobStorageService = blobStorageService;
        }

        [HttpGet]
        public async Task<ActionResult> Download(String fileName)
        {
            var stream = await _blobStorageService.DownloadFile(fileName);
            if (stream == null) return NotFound();

            return File(stream, "application/octet-stream", fileName);
        }
        [HttpPost]
        public async Task<ActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0) return BadRequest("No file uploaded");
            using var stream = file.OpenReadStream();
            await _blobStorageService.UploadFile(stream, file.FileName);
            return Ok("Fil Uploaded");
        }
    }
}

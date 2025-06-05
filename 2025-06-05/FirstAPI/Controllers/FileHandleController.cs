using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FirstAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FileHandleController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> FileUpload(IFormFile file)
    {
        var writingFile = System.IO.File.Create($"./Files/{file.FileName}");
        await file.CopyToAsync(writingFile);
        return Ok($"Successfully uploaded {file.FileName} of Size {file.Length}");
    }

    [HttpGet]
    public ActionResult FileDownload()
    {
        var path = "./Files/download_notes.md";
        var file = new FileStream(path, FileMode.Open);
        return File(file, "applocation/octet-stream", Path.GetFileName(path));
    }
}
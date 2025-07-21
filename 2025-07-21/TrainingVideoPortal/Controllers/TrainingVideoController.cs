using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrainingVideoPortal.Models;
using TrainingVideoPortal.Services;

[ApiController]
[Route("/api/videos")]
public class TrainingVideoController : ControllerBase
{
    private readonly TrainingVideoService _tvs;

    public TrainingVideoController(TrainingVideoService tvs)
    {
        _tvs = tvs;
    }

    [HttpPost("upload")]
    public async Task<ActionResult<TrainingVideo>> Add([FromForm]TrainingVideoAddRequestDTO dto)
    {
        Console.WriteLine(dto.Video);
        return Ok(await _tvs.UploadFile(dto));
    }

    [HttpGet("{id}/stream")]
    public async Task<ActionResult> Get(int id)
    {
        return Ok(await _tvs.Get(id));
    }

    [HttpGet]
    public async Task<ActionResult<List<TrainingVideo>>> GetAll()
    {
        return Ok(await _tvs.GetAll());
    }
}
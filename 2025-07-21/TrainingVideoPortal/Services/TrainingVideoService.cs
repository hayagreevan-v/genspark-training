using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using TrainingVideoPortal.Contexts;
using TrainingVideoPortal.Models;

namespace TrainingVideoPortal.Services;

public class TrainingVideoService
{
    private readonly TVPContext _context;
    private readonly IConfiguration _configuration;
    private BlobContainerClient _containerClient;
    public TrainingVideoService(TVPContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
        _containerClient = new BlobContainerClient(configuration["AzureBlob:ConnectionString"]!,configuration["AzureBlob:ContainerName"]!);
    }

    public async Task<TrainingVideo> UploadFile(TrainingVideoAddRequestDTO dto)
    {
        var blobClient = _containerClient.GetBlobClient($"{Guid.NewGuid()}_{dto.Video?.FileName}");
        await blobClient.UploadAsync(dto.Video?.OpenReadStream(), overwrite: true);

        var metaData = new TrainingVideo
        {
            Title = dto.Title,
            Description = dto.Description,
            CreatedAt = DateTime.UtcNow,
            BlobUrl = blobClient.Uri.ToString()
        };
        await _context.AddAsync(metaData);
        await _context.SaveChangesAsync();
        return metaData;
    }

    public async Task<List<TrainingVideo>> GetAll()
    {
        return await _context.TrainingVideos.ToListAsync();
    }
    public async Task<TrainingVideo?> Get(int id)
    {
        return await _context.TrainingVideos.FindAsync(id);
    }
}
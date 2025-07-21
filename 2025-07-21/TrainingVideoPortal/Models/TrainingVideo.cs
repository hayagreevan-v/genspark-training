namespace TrainingVideoPortal.Models;

public class TrainingVideo
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string BlobUrl { get; set; } = string.Empty;

}
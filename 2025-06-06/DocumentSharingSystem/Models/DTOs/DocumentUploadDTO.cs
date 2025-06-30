namespace DocumentSharingSystem.Models.DTOs;

public class DocumentUploadDTO
{
    public string? Description { get; set; }
    public int TeamID { get; set; } = 0;
    public IFormFile? formFile { get; set; }
}
namespace DocumentSharingSystem.Models.DTOs;

public class DocumentFilterModel
{
    public string? SearchByOriginalFileName { get; set; }
    public DateTime? SearchByCreatedTime { get; set; }
    public Guid? SearchByCreatedUserId { get; set; }
    public string? SortBy { get; set; }
}
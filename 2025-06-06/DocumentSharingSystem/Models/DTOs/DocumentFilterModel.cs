namespace DocumentSharingSystem.Models.DTOs;

public class DocumentFilterModel
{
    public string? SearchByOriginalFileName { get; set; }
    public DateTime? SearchByCreatedTime { get; set; }
    public string? SearchByCreatedUserEmail { get; set; }
    public string? SortBy { get; set; }
    public string? SortOrder { get; set; }
    public int? PageNo { get; set; }
    public int? PageSize { get; set; }
    public string? View { get; set; }
}
using System;

namespace DocumentSharingSystem.Models.DTOs;

public class DocumentReponseDTO
{
    public Guid Id { get; set; } = Guid.Empty;
    public string StoredFileName { get; set; } = string.Empty;
    public string OriginalFileName { get; set; } = string.Empty;
    // public bool IsDeleted { get; set; } = false;
    public Guid CreatedByUserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Guid LastUpdatedByUserId { get; set; }
    public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;
}
using System;

namespace DocumentSharingSystem.Models.DTOs;

public class TeamResponseDTO
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsDeleted { get; set; } = false;
    public Guid CreatedByUserId { get; set; }
    public string CreatedByUserName { get; set; } = string.Empty;
    public string CreatedByUserEmail { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Guid LastUpdatedByUserId { get; set; }
    public string LastUpdatedByUserName { get; set; } = string.Empty;
    public string LastUpdatedByUserEmail { get; set; } = string.Empty;
    public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;
}

using System;
using DocumentSharingSystem.Misc;

namespace DocumentSharingSystem.Models.DTOs;

public class UserUpdateRequestDTO
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public long TeamId { get; set; }
    public string? Password { get; set; } = string.Empty;
}

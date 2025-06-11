using System;

namespace DocumentSharingSystem.Models.DTOs;


public class LoginResponseDTO
{
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public Guid RefreshToken { get; set; }
}

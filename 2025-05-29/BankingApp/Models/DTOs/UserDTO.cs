using System;

namespace BankingApp.Models.DTOs;

public class UserDTO
{
    public string Name { get; set; } = string.Empty;
    public double Balance { get; set; }
}

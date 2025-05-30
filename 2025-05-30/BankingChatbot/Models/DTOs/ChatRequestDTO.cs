using System;

namespace BankingChatbot.Models.DTOs;

public class ChatRequestDTO
{
    public Guid? Uuid { get; set; }
    public string Prompt { get; set; } = string.Empty;
}

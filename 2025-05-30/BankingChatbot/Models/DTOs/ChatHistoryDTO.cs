using System;
using Mscc.GenerativeAI;

namespace BankingChatbot.Models.DTOs;

public class ChatHistoryDTO
{
    public string Context { get; set; } = string.Empty;
    public List<ContentResponse>? History { get; set; }
}

using System;
using Mscc.GenerativeAI;

namespace BankingChatbot.Models;

public class User
{
    public Guid UUID { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<ContentResponse>? ChatHistory { get; set; }
}

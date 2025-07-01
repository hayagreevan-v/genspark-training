using System;
using Microsoft.AspNetCore.SignalR;

namespace DocumentSharingSystem.Misc;

public class NotificationHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("RecieveMessage", user, message);
    }
    public async Task SendTeamMessage(string user, string message, long? TeamId)
    {
        await Clients.All.SendAsync("RecieveTeamMessage", user, message, TeamId);
    }
    public override async Task OnConnectedAsync()
    {
        Console.WriteLine($"Client connected: {Context.ConnectionId}");
        await base.OnConnectedAsync();
    }
    public override async Task OnDisconnectedAsync(Exception? exception)
{
    Console.WriteLine($"Client disconnected: {Context.ConnectionId}");
    await base.OnDisconnectedAsync(exception);
}
}

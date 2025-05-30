using System;
using BankingChatbot.Models;
using BankingChatbot.Models.DTOs;
using Mscc.GenerativeAI;

namespace BankingChatbot.Repositories;

public class UserRepository
{
    static readonly List<User> list = new List<User>();
    public User Add(UserCreateDTO dto)
    {
        User user = new User
        {
            Name = dto.Name,
            UUID = Guid.NewGuid(),
            ChatHistory = new List<ContentResponse>()
        };
        list.Add(user);
        return user;
    }
    public User Get(Guid uuid)
    {
        User? user = list.FirstOrDefault(l => l.UUID == uuid);
        if (user == null) throw new Exception("User not found");
        return user;
    }

    public List<User> GetAll()
    {
        if (list.Count() == 0) throw new Exception("No users found");
        return list;
    }
}

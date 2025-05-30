using System;
using BankingChatbot.Models;
using BankingChatbot.Models.DTOs;
using BankingChatbot.Repositories;
using Mscc.GenerativeAI;

namespace BankingChatbot.Services;

public class ChatService
{
    private readonly ChatRepository _ChatRepository;
    private readonly UserRepository _UserRepository;
    public ChatService(ChatRepository ChatRepository, UserRepository userRepository)
    {
        _ChatRepository = ChatRepository;
        _UserRepository = userRepository;   
    }

    public async Task<string> Chat(ChatRequestDTO dto)
    {
        try
        {
            if (dto.Uuid != null)
            {
                User user = _UserRepository.Get((Guid)dto.Uuid);
                ChatHistoryDTO chatHistoryDTO = new ChatHistoryDTO { Context = dto.Prompt, History = user.ChatHistory };
                chatHistoryDTO = await _ChatRepository.Chat(chatHistoryDTO);
                user.ChatHistory = chatHistoryDTO.History;

                return chatHistoryDTO.Context;
            }
            else
            {
                ChatHistoryDTO chatHistoryDTO = new ChatHistoryDTO { Context = dto.Prompt};
                chatHistoryDTO = await _ChatRepository.Chat(chatHistoryDTO);
                return chatHistoryDTO.Context;
            }
        }
        catch
        {
            throw;
        } 
    }
}

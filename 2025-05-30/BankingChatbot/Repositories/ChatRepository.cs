using BankingChatbot.Contexts;
using BankingChatbot.Models.DTOs;
using Mscc.GenerativeAI;

namespace BankingChatbot.Repositories
{
    public class ChatRepository
    {
        private readonly GenAIContext _genAIContext;
        public ChatRepository(GenAIContext genAIContext)
        {
            _genAIContext = genAIContext;
        }

        public async Task<ChatHistoryDTO> Chat(ChatHistoryDTO dto)
        {
            ChatSession chatSession;
            GenerateContentRequest req = _genAIContext.CreateRequest(dto.Context);
            if (dto.History != null)
            {
                chatSession = _genAIContext.CreateChatSession(dto.History);
            }
            else
            {
                chatSession = _genAIContext.CreateChatSession(null);
            }
            var res = await chatSession.SendMessage(req);
            if (res == null || res.Text==null) throw new Exception("No response");
            dto.History = chatSession.History;
            dto.Context = res.Text;
            return dto;
        }
    }
}
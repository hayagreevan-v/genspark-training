using BankingChatbot.Models.DTOs;
using BankingChatbot.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankingChatbot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ChatService _chatService;
        private readonly UserService _userService;
        public ChatController(ChatService chatService, UserService userService)
        {
            _chatService = chatService;
            _userService = userService;
        }
        [HttpPost]
        public async Task<ActionResult<string>> Chat(ChatRequestDTO dto)
        {
            try
            {
                string? output = await _chatService.Chat(dto);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("uuid")]
        public ActionResult Clear(Guid uuid)
        {
            try
            {
                _userService.Clear(uuid);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
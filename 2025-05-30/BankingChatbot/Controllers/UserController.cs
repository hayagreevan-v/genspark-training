using BankingChatbot.Models;
using BankingChatbot.Models.DTOs;
using BankingChatbot.Repositories;
using BankingChatbot.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankingChatbot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public ActionResult<User> GetAll()
        {
            try
            {
                List<User>? users = _userService.GetAll();
                if (users == null || users.Count() == 0) return NotFound("No user found");
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("uuid")]
        public ActionResult<User> Get(Guid uuid)
        {
            try
            {
                User? user = _userService.Get(uuid);
                if (user == null) return NotFound("No user found");
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<User> Add(UserCreateDTO dto)
        {
            try
            {
                User? user = _userService.Add(dto);
                return Created("", user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }
}

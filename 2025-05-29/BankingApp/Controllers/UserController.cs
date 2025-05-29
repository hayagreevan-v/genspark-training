using System.Globalization;
using BankingApp.Models;
using BankingApp.Models.DTOs;
using BankingApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<ActionResult<ICollection<User>>> GetUsers()
        {
            try
            {
                var result = await _userService.ViewAll();
                if (result == null) return NotFound("No users found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            try
            {
                var result = await _userService.View(id);
                if (result == null) return NotFound("No users found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<User>> Add([FromBody] UserDTO userDTO)
        {
            try
            {
                User newUser = await _userService.Add(userDTO);
                if (newUser == null) return BadRequest("User cannot be created!");
                return Created("", newUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

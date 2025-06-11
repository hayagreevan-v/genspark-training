using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DocumentSharingSystem.Models.DTOs;
using DocumentSharingSystem.Services;
using DocumentSharingSystem.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using DocumentSharingSystem.Models.DTOs.CustomResponseDTOs;
using AutoMapper;

namespace DocumentSharingSystem.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationService _authenticationService;
        private readonly UserService _userService;
        private readonly IMapper _mapper;
        private readonly CustomResponseGeneration _res;
        public AuthenticationController(AuthenticationService authenticationService, UserService userService, CustomResponseGeneration customResponseGeneration, IMapper mapper)
        {
            _authenticationService = authenticationService;
            _userService = userService;
            _res = customResponseGeneration;
            _mapper = mapper;
        }
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDTO>> Login([FromBody]LoginRequestDTO dto)
        {
            var result = await _authenticationService.Login(dto);
            return Ok(result);
        }
        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<CustomResponseDTO<UserResponseDTO>>> GetLoggedInUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return NotFound("User not logged in!");
            User user = await _userService.GetUser(Guid.Parse(userId));
            UserResponseDTO userDTO = _mapper.Map<User, UserResponseDTO>(user);
            return _res.Generate<UserResponseDTO>(userDTO, "Logged In User Details");
        }
        
        [HttpPost("refresh")]
        public async Task<ActionResult<LoginResponseDTO>> Refresh(Guid token)
        {
            var dto = await _authenticationService.Refresh(token);
            if (dto == null) return NotFound("Token Expired");
            return dto;
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            var userId= User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized("User not logged in!");

            bool success = await _authenticationService.Logout(Guid.Parse(userId));
            if (!success) return NotFound("Token Expired");
            return Ok("User logged out sucessfully");
        }
    }
}

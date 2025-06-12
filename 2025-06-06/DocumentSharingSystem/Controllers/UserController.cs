using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DocumentSharingSystem.Models;
using DocumentSharingSystem.Models.DTOs;
using DocumentSharingSystem.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using DocumentSharingSystem.Models.DTOs.CustomResponseDTOs;
using AutoMapper;

namespace DocumentSharingSystem.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IMapper _mapper;
        private readonly CustomResponseGeneration _res;
        public UserController(UserService userService, CustomResponseGeneration customResponseGeneration, IMapper mapper)
        {
            _userService = userService;
            _res = customResponseGeneration;
            _mapper = mapper;
        }
        [HttpGet("all")]
        public async Task<ActionResult<ICollection<UserResponseDTO>>> GetAll()
        {
            var role = User.FindFirstValue(ClaimTypes.Role);
            try
            {
                if (role == "Admin")
                {
                    var users = await _userService.GetAll_Admin();
                    var usersRes = users.Select(u => _mapper.Map<User, UserResponseDTO>(u));
                    return Ok(usersRes);
                }
                else if (role == "User")
                {
                    {
                    var users = await _userService.GetAll();
                    var usersRes = users.Select(u => _mapper.Map<User, UserResponseDTO>(u));
                    return Ok(usersRes);
                }
                }
                else
                    throw new Exception("UnAuthorized User");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("page")]
        public async Task<ActionResult<CustomPaginationDTO<UserResponseDTO>>> GetWithPagination(int pageNo=1, int pageSize=10)
        {
            var role = User.FindFirstValue(ClaimTypes.Role);
            try
            {
                if (role == "Admin")
                {
                    var users_dto = await _userService.UsersPagination_Admin(pageNo, pageSize);
                    if (users_dto == null || users_dto.Data == null ) throw new Exception("No records found");
                    var users = users_dto.Data.Select(u => _mapper.Map<User, UserResponseDTO>(u)).ToList();
                    var usersRes =_res.GeneratePagination_User(users, pageNo, pageSize, users_dto.TotalRecords, "Succesfully fetched");

                    return Ok(usersRes);
                }
                else if (role == "User")
                {
                    {
                    var users_dto = await _userService.UsersPagination(pageNo, pageSize);
                    if (users_dto == null || users_dto.Data == null) throw new Exception("No records found");
                    var users = users_dto.Data.Select(u => _mapper.Map<User, UserResponseDTO>(u)).ToList();
                    var usersRes =_res.GeneratePagination_User(users, pageNo, pageSize, users_dto.TotalRecords, "Succesfully fetched");

                    return Ok(usersRes);
                }
                }
                else
                    throw new Exception("UnAuthorized User");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomResponseDTO<User>>> Get(Guid id)
        {
            var role = User.FindFirstValue(ClaimTypes.Role);
            try
            {
                if (role == "Admin")
                    return Ok(_res.Generate<UserResponseDTO>( _mapper.Map<User,UserResponseDTO>(await _userService.GetUser_Admin(id)), "Item fetched successfully"));
                else if (role == "User")
                    return Ok(_res.Generate<UserResponseDTO>( _mapper.Map<User,UserResponseDTO>(await _userService.GetUser(id)), "Item fetched successfully"));
                else
                    throw new Exception("UnAuthorized User");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("email/{email}")]
        public async Task<ActionResult<CustomResponseDTO<User>>> Get(string email)
        {
            var role = User.FindFirstValue(ClaimTypes.Role);
            try
            {
                if (role == "Admin")
                    return Ok(_res.Generate<UserResponseDTO>( _mapper.Map<User,UserResponseDTO>(await _userService.GetUserByEmail_Admin(email)), "Item fetched successfully"));
                else if (role == "User")
                    return Ok(_res.Generate<UserResponseDTO>( _mapper.Map<User,UserResponseDTO>(await _userService.GetUserByEmail(email)), "Item fetched successfully"));
                else
                    throw new Exception("UnAuthorized User");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<CustomResponseDTO<UserResponseDTO>>> AddUser([FromBody]UserAddRequestDTO dto)
        {
            UserAddServiceDTO serviceDTO = _mapper.Map<UserAddRequestDTO, UserAddServiceDTO>(dto);
            var createdUser = await _userService.GetUserByEmail(User.FindFirst(ClaimTypes.Email)!.Value);
            serviceDTO.CreatedByUserId = createdUser.Id;
            User user = await _userService.AddUser(serviceDTO);
            UserResponseDTO output = _mapper.Map<User, UserResponseDTO>(user);
            return Ok(_res.Generate<UserResponseDTO>(output,"User Added Successfully"));
        }

        [HttpPut("{id}")]
        [Authorize(Policy ="SpecifiedUserOrAdmin")]
        public async Task<ActionResult<CustomResponseDTO<UserResponseDTO>>> UpdateUser(Guid id, [FromBody]UserUpdateRequestDTO dto)
        {
            UserAddServiceDTO serviceDTO = _mapper.Map<UserUpdateRequestDTO, UserAddServiceDTO>(dto);
            var createdUser = await _userService.GetUserByEmail(User.FindFirst(ClaimTypes.Email)!.Value);
            serviceDTO.CreatedByUserId = createdUser.Id;
            User user = await _userService.UpdateUser(id, serviceDTO);
            UserResponseDTO output = _mapper.Map<User, UserResponseDTO>(user);
            return Ok(_res.Generate<UserResponseDTO>(output,"User Updated Successfully"));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomResponseDTO<UserResponseDTO>>> DeleteUser(Guid id)
        {
            var user = await _userService.GetUser(id);
            user = await _userService.DeleteUser(id, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!));
            UserResponseDTO output = _mapper.Map<User, UserResponseDTO>(user);
            return Ok(_res.Generate<UserResponseDTO>(output,"User Deleted Successfully"));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("revoke/{id}")]
        public async Task<ActionResult<CustomResponseDTO<UserResponseDTO>>> RevokeDeletedUser(Guid id, string? role)
        {
            var updatedByUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (updatedByUserId == null) return Unauthorized("Unauthorized Access");
            var revokedUser = await _userService.RevokeDeletedUser(id, role, Guid.Parse(updatedByUserId));

            UserResponseDTO output = _mapper.Map<User, UserResponseDTO>(revokedUser);
            return Ok(_res.Generate<UserResponseDTO>(output,"User Revoked Successfully"));
        }
    }
}

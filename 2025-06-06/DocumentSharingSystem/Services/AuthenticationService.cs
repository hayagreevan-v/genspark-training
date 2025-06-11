using System;
using System.Text;
using DocumentSharingSystem.Models;
using DocumentSharingSystem.Models.DTOs;

namespace DocumentSharingSystem.Services;

public class AuthenticationService
{
    private readonly UserService _userService;
    private readonly TokenService _tokenService;
    private readonly RefreshTokenService _refreshTokenService;
    public AuthenticationService(UserService userService, TokenService tokenService, RefreshTokenService refreshTokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
        _refreshTokenService = refreshTokenService;
    }
    public async Task<LoginResponseDTO> Login(LoginRequestDTO dto)
    {
        User user = await _userService.GetUserByEmail(dto.Email);
        if (user == null) throw new Exception("No user found");
        // var hashedData = new HashDTO { Data = dto.Password };
        // hashedData = _hashingService.HashData(hashedData);
        // if (user.Password!.ToString()!.Equals(hashedData.HashedData!.ToString(), StringComparison.OrdinalIgnoreCase))
        if (BCrypt.Net.BCrypt.EnhancedVerify(dto.Password, Encoding.UTF8.GetString(user.Password!)))
        {
            string AccessToken = _tokenService.GenerateToken(user.Id, user.Email, user.Role);
            RefreshToken rt;
            try
            {
                rt = await _refreshTokenService.GetTokenByUserId(user.Id);
            }
            catch
            {
                rt = await _refreshTokenService.CreateToken(user.Id);
            }

            LoginResponseDTO res = new LoginResponseDTO
            {
                Email = user.Email,
                Role = user.Role,
                AccessToken = AccessToken,
                RefreshToken = rt.Token
            };
            return res;
        }
        else
        {
            throw new Exception("Invalid Password");
        }
    }

    public async Task<bool> Logout(Guid id)
    {
        return await _refreshTokenService.RemoveToken(id);
    }

    public async Task<LoginResponseDTO> Refresh(Guid id)
    {
        var rt = await _refreshTokenService.GetToken(id);
        if (rt == null) throw new Exception("Token expired");

        User user = await _userService.GetUser(rt.UserId);
        if (user == null) throw new Exception("No user found");

        string AccessToken = _tokenService.GenerateToken(user.Id, user.Email, user.Role);
        LoginResponseDTO res = new LoginResponseDTO
        {
            Email = user.Email,
            Role = user.Role,
            AccessToken = AccessToken,
            RefreshToken = rt.Token
        };
        return res;
    }
}

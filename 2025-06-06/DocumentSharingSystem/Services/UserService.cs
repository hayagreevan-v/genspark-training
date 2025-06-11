using System;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BCrypt.Net;
using DocumentSharingSystem.Interfaces;
using DocumentSharingSystem.Misc;
using DocumentSharingSystem.Models;
using DocumentSharingSystem.Models.DTOs;
using DocumentSharingSystem.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace DocumentSharingSystem.Services;

[Authorize]
public class UserService
{
    private readonly IRepo<Guid, User> _userRepo;
    private readonly IMapper _mapper;
    private readonly PaginationContextFns _paginationContextFns;
    public UserService(IRepo<Guid, User> repo, IMapper mapper, PaginationContextFns paginationContextFns)
    {
        _userRepo = repo;
        _mapper = mapper;
        _paginationContextFns = paginationContextFns;
    }
    [Authorize(Roles = "Admin")]
    public async Task<User> AddUser(UserAddServiceDTO dto)
    {
        // DateTime dateTime = DateTime.UtcNow;
        // User user = new User
        // {
        //     Id = Guid.NewGuid(),
        //     Name = dto.Name,
        //     Email = dto.Email,
        //     Role = dto.Role,
        //     CreatedAt = dateTime,
        //     LastUpdatedAt = dateTime,
        //     CreatedByUserId = dto.CreatedByUserId,
        //     LastUpdatedByUserId = dto.CreatedByUserId
        // };
        User user = _mapper.Map<UserAddServiceDTO, User>(dto);
        string pwd = BCrypt.Net.BCrypt.EnhancedHashPassword(dto.Password, 13);
        user.Password = Encoding.UTF8.GetBytes(pwd);

        user = await _userRepo.Add(user);
        return user;
    }
    public async Task<User> UpdateUser(Guid userId, UserAddServiceDTO dto)
    {
        DateTime dateTime = DateTime.UtcNow;

        User user = await _userRepo.Get(userId);
        if (user == null) throw new Exception("No user found");

        user.Name = dto.Name;
        user.Email = dto.Email;
        user.LastUpdatedAt = dateTime;
        user.LastUpdatedByUserId = dto.CreatedByUserId;

        string pwd = BCrypt.Net.BCrypt.EnhancedHashPassword(dto.Password, 13);
        user.Password = Encoding.UTF8.GetBytes(pwd);

        user = await _userRepo.Update(userId, user);
        return user;
    }

    public async Task<User> GetUser(Guid id)
    {
        var user = await _userRepo.Get(id);
        if (user == null) throw new Exception("No user found");
        return user;
    }
    public async Task<User> GetUser_Admin(Guid id)
    {
        var users = await _userRepo.GetAll();
        // users = users.Where(u => !u.IsDeleted).ToList();
        var user = users.FirstOrDefault(u => u.Id == id);
        if (user == null) throw new Exception("No user found");
        return user;
    }
    public async Task<User> GetUserByEmail(string email)
    {
        var users = await _userRepo.GetAll();
        users = users.Where(u => !u.IsDeleted).ToList();
        var user = users.FirstOrDefault(u => u.Email == email);
        if (user == null) throw new Exception("No user found");
        return user;
    }
    public async Task<User> GetUserByEmail_Admin(string email)
    {
        var users = await _userRepo.GetAll();
        // users = users.Where(u => !u.IsDeleted).ToList();
        var user = users.FirstOrDefault(u => u.Email == email);
        if (user == null) throw new Exception("No user found");
        return user;
    }
    public async Task<ICollection<User>> GetAll()
    {
        var users = await _userRepo.GetAll();
        users = users.Where(u => !u.IsDeleted).ToList();
        if (users == null || users.Count() == 0) throw new Exception("No user found");
        return users;
    }
    public async Task<ICollection<User>> GetAll_Admin()
    {
        var users = await _userRepo.GetAll();
        if (users == null || users.Count() == 0) throw new Exception("No user found");
        return users;
    }

    public async Task<User> DeleteUser(Guid id, Guid deletedByserId)
    {
        var user = await _userRepo.Get(id);
        if (user == null) throw new Exception("No user found");
        user = await _userRepo.Delete(id, deletedByserId);

        return user;
    }

    public async Task<PaginationDataDTO<User>> UsersPagination_Admin(int pageNo, int pageSize)
    {
        return await _paginationContextFns.UsersPagination_Admin(pageNo, pageSize);
    }
    public async Task<PaginationDataDTO<User>> UsersPagination(int pageNo, int pageSize)
    {
        return await _paginationContextFns.UsersPagination(pageNo, pageSize);
    }
}

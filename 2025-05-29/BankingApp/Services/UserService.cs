using System;
using BankingApp.Contexts;
using BankingApp.Interfaces;
using BankingApp.Mappers;
using BankingApp.Models;
using BankingApp.Models.DTOs;
using BankingApp.Repositories;

namespace BankingApp.Services;

public class UserService : IService<int,User, UserDTO>
{
    private readonly IRepository<int,User> _userRepository;

    public UserService(IRepository<int,User> userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<User> Add(UserDTO item)
    {
        User newUser = UserMapper.UserFromUserDTO(item);
        try
        {
            newUser = await _userRepository.Add(newUser);
            return newUser;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<User> View(int AccountNo)
    {
        User? user = await _userRepository.Get(AccountNo);
        if (user == null) throw new Exception("No user found");
        return user;
    }

    public async Task<ICollection<User>> ViewAll()
    {
        return await _userRepository.GetAll();
    }
}

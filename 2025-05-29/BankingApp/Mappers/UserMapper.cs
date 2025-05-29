using System;
using BankingApp.Models;
using BankingApp.Models.DTOs;

namespace BankingApp.Mappers;

public class UserMapper
{
    public static User UserFromUserDTO(UserDTO userDTO)
    {
        return new User
        {
            Name = userDTO.Name,
            Balance = userDTO.Balance
        };
    }
}

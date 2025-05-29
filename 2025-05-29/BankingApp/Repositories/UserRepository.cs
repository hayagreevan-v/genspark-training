using System;
using BankingApp.Contexts;
using BankingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Repositories;

public class UserRepository : Repository<int, User>
{
    public UserRepository(BankContext bankContext) : base(bankContext){}

    public override async Task<User> Get(int id)
    {
        var user = await _bankContext.Users.FindAsync(id);
        if (user == null) throw new Exception("No data found");
        return user;
    }

    public override async Task<ICollection<User>> GetAll()
    {
        return await _bankContext.Users.ToListAsync();
    }
}

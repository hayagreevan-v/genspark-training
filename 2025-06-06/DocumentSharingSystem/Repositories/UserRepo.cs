using System;
using Microsoft.EntityFrameworkCore;
using DocumentSharingSystem.Contexts;
using DocumentSharingSystem.Models;

namespace DocumentSharingSystem.Repositories;

public class UserRepo : Repo<Guid, User>
{
    public UserRepo(DocumentSharingSystemContext DocumentSharingSystemContext) : base(DocumentSharingSystemContext) { }


    public override async Task<User> Get(Guid id)
    {
        User? user = await _context.users.FindAsync(id);
        if (user == null || user.IsDeleted) throw new Exception("User not found");
        return user;
    }

    public override async Task<ICollection<User>> GetAll()
    {
        var users = _context.users;
        if (users == null) throw new Exception("No users found");
        return await users.ToListAsync();
    }
}

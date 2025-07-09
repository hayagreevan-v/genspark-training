using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VMDemo.Contexts;
using VMDemo.Models;
using AppContext = VMDemo.Contexts.AppContext;

namespace VMDemo.Repositories;

public class UserRepo
{
    private AppContext _context;
    public UserRepo(AppContext appContext)
    {
        _context = appContext;
    }

    public async Task<User> Add(User newUser)
    {
        await _context.AddAsync(newUser);
        await _context.SaveChangesAsync();
        // Console.WriteLine("Added user");
        return newUser;
    }
    public async Task<User> Get(int id)
    {
        return await _context.users.FindAsync(id)??throw new Exception("No user found");
    }
    public async Task<List<User>> GetAll()
    {
        return await _context.users.ToListAsync();
    }
    public async Task<User> Delete(int id)
    {
        User u = await Get(id);
        _context.Remove(u);
        await _context.SaveChangesAsync();
        return u;
    }
}
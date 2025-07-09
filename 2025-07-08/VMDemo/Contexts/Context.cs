using Microsoft.EntityFrameworkCore;
using VMDemo.Models;

namespace VMDemo.Contexts;
public class AppContext : DbContext
{
    public AppContext(DbContextOptions options) : base(options)
    {

    }
    public DbSet<User> users { get; set; }
}
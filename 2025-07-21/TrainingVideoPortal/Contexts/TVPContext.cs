using Microsoft.EntityFrameworkCore;
using TrainingVideoPortal.Models;

namespace TrainingVideoPortal.Contexts;

public class TVPContext : DbContext
{
    public TVPContext(DbContextOptions options) : base(options){}
    public DbSet<TrainingVideo> TrainingVideos { get; set; }
}
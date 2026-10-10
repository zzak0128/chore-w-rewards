using KidsPointTracker.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace KidsPointTracker.Shared.Database;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Reward> Rewards { get; set; }

    public DbSet<History> Histories { get; set; }

    public DbSet<TaskItem> TaskItems { get; set; }
    
    public ApplicationDbContext(DbContextOptions options) : base(options) { }
}
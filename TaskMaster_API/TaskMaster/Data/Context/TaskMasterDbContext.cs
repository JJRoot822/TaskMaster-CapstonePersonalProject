using Microsoft.EntityFrameworkCore;

using TaskMaster.Model;

namespace TaskMaster.Data.Context;

public class TaskMasterDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> Roles { get; set; }

    public TaskMasterDbContext(DbContextOptions<TaskMasterDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure your entity models here
    }
}


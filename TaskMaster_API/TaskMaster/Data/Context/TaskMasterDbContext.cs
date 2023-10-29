using Microsoft.EntityFrameworkCore;

using TaskMaster.Model.Domain.User;

namespace TaskMaster.Data.Context;

public class TaskMasterDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> Roles { get; set; }

    public TaskMasterDbContext(DbContextOptions<TaskMasterDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

builder.Entity<UserRole>().HasData(
            new UserRole { UserRoleId = 1, RoleName = "Admin", RoleDetails = "The Administratorrole  is the highest role you are able to obtain." },
            new UserRole { UserRoleId = 2, RoleName = "Manager", RoleDetails = "The Manager role is the second highest role you can obtain." },
            new UserRole { UserRoleId = 3, RoleName = "Quality Assurance Tester", RoleDetails = "The QA testing is the third highest role you can obtain" },
            new UserRole {  UserRoleId = 4, RoleName = "Developer", RoleDetails = "The Developer role is the fourth highest role you can obtain." },
            new UserRole {  UserRoleId = 5, RoleName = "IT Tech", RoleDetails = "The IT tech is the fifth highest role you can obtain." },
            new UserRole {  UserRoleId = 6, RoleName = "Client", RoleDetails = "The client role is the lowest role." }
        );
    }

}


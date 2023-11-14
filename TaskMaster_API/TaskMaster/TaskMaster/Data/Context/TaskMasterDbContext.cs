using Microsoft.EntityFrameworkCore;

using TaskMaster.Model.Domain.ProjectData;
using TaskMaster.Model.Domain.ProjectData.TaskitemData;
using TaskMaster.Model.Domain.UserData;
using TaskMaster.Services.Security;

namespace TaskMaster.Data.Context;

public class TaskMasterDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> Roles { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<TaskItem> TaskItems { get; set; }
    public DbSet<BugReport> BugReports { get; set; }
    public DbSet<IssueReport> IssueReports { get; set; }
    public DbSet<TestCase> TestCases { get; set; }

    public TaskMasterDbContext(DbContextOptions<TaskMasterDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

builder.Entity<UserRole>().HasData(
            new UserRole { UserRoleId = 1, RoleName = "Admin", RoleDetails = "The Administratorrole  is the highest role you are able to obtain." },
            new UserRole {  UserRoleId = 2, RoleName = "Developer", RoleDetails = "The Developer role is the second highest role you can obtain." }
        );

        builder.Entity<User>().HasData(
            new User
            {
                UserId = 1,
                FirstName = "Joshua",
                LastName = "Root",
                Username = "JJRoot822",
                Email = "jroot@example.com",
                Password = SecurityService.HashPassword("This_1s_$ecure_1357924680"),
                UserRoleId = 1
            }
        ); ;
    }

}


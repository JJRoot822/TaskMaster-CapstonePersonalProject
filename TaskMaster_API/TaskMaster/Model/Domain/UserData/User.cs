using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

using TaskMaster.Model.Domain.ProjectData;
using TaskMaster.Model.Domain.ProjectData.TaskitemData;

namespace TaskMaster.Model.Domain.UserData;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { set; get; }

    [Required]
    public string FirstName { set; get; }

    [Required]
    public string LastName { set; get; }

    [Required]
    public string Username { set; get; }

    [Required]
    public string Email { set; get; }

    [Required]
    public string Password { set; get; }

    [Required]
    [ForeignKey("UserRole")]
    public int UserRoleId { get; set; }
    public UserRole Role { set; get; }

    public List<Project> Projects { get; set; }
    public List<TaskItem> TaskItems { get; set; }
    public List<TaskItemComment> TaskItemComments { get; set; }
    public List<BugReport> BugReports { get; set; }
    public List<IssueReport> IssueReports { get; set; }
    public List<TestCase> TestCases { get; set; }

}

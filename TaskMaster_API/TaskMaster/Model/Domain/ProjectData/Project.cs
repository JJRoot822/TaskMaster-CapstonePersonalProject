using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using TaskMaster.Model.Domain.UserData;
using TaskMaster.Model.Domain.ProjectData.TaskitemData;

namespace TaskMaster.Model.Domain.ProjectData;

public class Project
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProjectId { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Color { get; set; }
    
    public string Description { get; set; }

    [Required]
    public DateTime ReleaseDate { get; set; }
    
    [Required]
        [ForeignKey("User")]
    public int UserId { get; set; }

    public User User { get; set; }

    public List<TaskItem> TaskItems { get; set; }
    public List<BugReport> BugReports { get; set; }
    public List<APIIssueReport> APIIssues { get; set; }
    public List<TestCase> TestCases { get; set; }
}

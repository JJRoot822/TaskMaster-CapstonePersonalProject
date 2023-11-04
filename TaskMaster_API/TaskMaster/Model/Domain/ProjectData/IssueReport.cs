using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.Identity.Client;

using TaskMaster.Model.API.ProjectData;
using TaskMaster.Model.API.UserData;
using TaskMaster.Model.Domain.UserData;

namespace TaskMaster.Model.Domain.ProjectData;

public class IssueReport
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IssueReportId { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public int Priority { get; set; }

    [Required]
    public int Severity { get; set; }

    [Required]
    public int IssueType { get; set; }

    [Required]
    public string Details { get; set; }

    [Required]
    public bool Fixed { get; set; }

    public DateTime DateResolved { get; set; }
    
    public string Notes { get; set; }

    [Required]
    [ForeignKey("User")]
    public int UserId { get; set; }

    [Required]
    [ForeignKey("Project")]
    public int ProjectId { get; set; }

    public User User { get; set; }
    public Project Project { get; set; }    
}

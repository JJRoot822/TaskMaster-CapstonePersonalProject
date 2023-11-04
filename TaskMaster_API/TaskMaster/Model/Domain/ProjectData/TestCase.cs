using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using TaskMaster.Model.API.ProjectData;
using TaskMaster.Model.Domain.UserData;

namespace TaskMaster.Model.Domain.ProjectData;

public class TestCase
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TestCaseId { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Details { get; set; }

    [Required]
    [ForeignKey("User")]
    public int UserId { get; set; }

    [Required]
    [ForeignKey("Project")]
    public int ProjectId { get; set; }

    public Project Project { get; set; }
    public User User { get; set; }

    
}

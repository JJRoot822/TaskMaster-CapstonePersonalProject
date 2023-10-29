using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskMaster.Model.Domain.ProjectData.Taskitem;

public class TaskItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TaskItemId { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public int Priority { get; set; }
    
    [Required]
    public bool Completed { get; set; }

    [Required]
    public string Notes { get; set; }

    [Required]
    public string Details { get; set; }

    [Required]
    public DateTime DueDate { get; set; }

    public DateTime CompletedDate { get; set; }

    [Required]
    [ForeignKey("Project")]
    public int ProjectId { get; set; }

    [Required
        [ForeignKey("User")]
        public int UserId { get; set; }

    public User User { get; set; }
    public Project Project { get; set; }
}

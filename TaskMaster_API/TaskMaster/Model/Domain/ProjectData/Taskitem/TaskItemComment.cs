using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskMaster.Model.Domain.ProjectData.Taskitem;

public class TaskItemComment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TaskCommentId { get; set; }

    [Required]
    public string Content { get; set; }

    [Required]
    public DateTime DatePosted { get; set; }

    [Required]
    [ForeignKey("TaskItem")]
    public int TaskItemId { get; set; }

    [Required]
    [ForeignKey("User")]
    public int UserId { get; set; }

    public Taskitem TaskItem {get; set; }
    public User User { get; set; }
}


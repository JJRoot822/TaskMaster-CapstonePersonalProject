using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using TaskMaster.Model.Domain.UserData;

namespace TaskMaster.Model.Domain.ProjectData.TaskitemData;

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

    public TaskItem TaskItem {get; set; }
    public User User { get; set; }
}


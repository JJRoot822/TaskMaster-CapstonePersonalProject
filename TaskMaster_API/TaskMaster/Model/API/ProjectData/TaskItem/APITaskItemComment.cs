namespace TaskMaster.Model.API.ProjectData.TaskItem;

public class APITaskItemComment
{
    public int TaskCommentId { get; set; }
    public string Content { get; set; }
    public DateTime DatePosted { get; set; }
    public int TaskItemId { get; set; }
    public int UserId { get; set; }
}

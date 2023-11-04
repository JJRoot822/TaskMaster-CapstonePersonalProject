namespace TaskMaster.Model.API.CrudOperations.TaskItemComment;

public class APICreateTaskItemComment
{
    public string Content { get; set; }
    public DateTime DatePosted { get; set; }
    public int TaskItemId { get; set; }
    public int UserId { get; set; }
}

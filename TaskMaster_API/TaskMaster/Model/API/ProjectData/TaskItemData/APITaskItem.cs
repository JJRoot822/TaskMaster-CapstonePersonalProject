using TaskMaster.Model.API.UserData;

namespace TaskMaster.Model.API.ProjectData.TaskItemData;

public class APITaskItem
{
    public int TaskItemId { get; set; }
    public string Title { get; set; }
    public int Priority { get; set; }
    public bool Completed { get; set; }
    public string Notes { get; set; }
    public string Details { get; set; }
    public DateTime CompletedDate { get; set; }
    public DateTime DueDate { get; set; }
    public APIProject Project { get; set; }
    public int UserId { get; set; }
}

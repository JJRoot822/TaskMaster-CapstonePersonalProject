using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaskMaster.Model.API.ProjectData.TaskItem;

public class APITaskItem
{
    public int TaskItemId { get; set; }
    public string Title { get; set; }
    public int Priority { get; set; }
    public bool Completed { get; set; }
    public string Notes { get; set; }
    public string Details { get; set; }
    public DateTime CompletedDate { get; set; 
    public DateTime DueDate { get; set; }
    public int ProjectId { get; set; }
    public int UserId { get; set; }
}

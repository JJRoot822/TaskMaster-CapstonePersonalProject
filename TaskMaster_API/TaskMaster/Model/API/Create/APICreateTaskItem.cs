namespace TaskMaster.Model.API.Create;

public class APICreateTaskItem
{
    public string Title { get; set; }

    public int Priority { get; set; }

    public bool Completed { get; set; }

    public string Notes { get; set; }

    public string Details { get; set; }

    public DateTime DueDate { get; set; }

    public DateTime? CompletedDate { get; set; }

    public int ProjectId { get; set; }

    public int UserId { get; set; }
}

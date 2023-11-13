namespace TaskMaster.Model.API.Update;

public class APIUpdateTaskItem
{
    public int taskItemId { get; set; }

    public string Title { get; set; }

    public int Priority { get; set; }

    public bool Completed { get; set; }

    public string Notes { get; set; }

    public string Details { get; set; }

    public DateTime DueDate { get; set; }

    public DateTime? CompletedDate { get; set; }
}

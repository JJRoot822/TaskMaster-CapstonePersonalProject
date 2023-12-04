namespace TaskMaster.Model.API;

public class APIProject
{
    public int ProjectId { get; set; }

    public string Name { get; set; }

    public string Color { get; set; }

    public string Description { get; set; }

    public DateTime ReleaseDate { get; set; }

    public int UserId { get; set; }

    public List<APITaskItem> Tasks { get; set; }
    
    public List<APIBugReport> Bugs { get; set; }
    
    public List<APIIssueReport> Issues { get; set; }
    
    public List<APITestCase> TestCases { get; set; }
}

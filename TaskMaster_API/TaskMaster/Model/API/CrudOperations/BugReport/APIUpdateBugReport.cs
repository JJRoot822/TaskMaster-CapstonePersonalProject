namespace TaskMaster.Model.API.CrudOperations.BugReport;

public class APIUpdateBugReport
{
    public int BugReportId { get; set; }
    public string Title { get; set; }
    public string Details { get; set; }
    public string Notes { get; set; }
    public int Priority { get; set; }
    public int Severity { get; set; }
    public bool Fixed { get; set; }
    public DateTime DateFixed { get; set; }
    
}

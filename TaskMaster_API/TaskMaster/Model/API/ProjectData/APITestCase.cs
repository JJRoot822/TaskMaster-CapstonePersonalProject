using TaskMaster.Model.API.UserData;

namespace TaskMaster.Model.API.ProjectData;

public class APITestCase
{
    public int TestCaseId { get; set; }
    public string Title { get; set; }
    public string Details { get; set; }
    public int ProjectId { get; set; }
    public int UserId { get; set; }
}


using System.ComponentModel.DataAnnotations.Schema;
using TaskMaster.Model.Domain.UserData;

namespace TaskMaster.Model.API;

public class APITestCase
{
    public int TestCaseId { get; set; }

    public string Title { get; set; }

    public string Details { get; set; }

    public int UserId { get; set; }

    public int ProjectId { get; set; }
}

using TaskMaster.Model.API.ProjectData;
using TaskMaster.Model.Domain.UserData;

namespace TaskMaster.Model.API.UserData;

public class APIUser
{
    public string UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public int UserRoleId { get; set; }
}

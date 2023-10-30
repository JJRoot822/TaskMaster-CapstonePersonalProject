using TaskMaster.Model.Domain.UserData;

namespace TaskMaster.Model.API.UserData;

public class APICreateUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; }
}

using TaskMaster.Model.Domain;

namespace TaskMaster.Model.API.User;

public class APIUser
{
    public string UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public UserRole Role { get; set; }
}

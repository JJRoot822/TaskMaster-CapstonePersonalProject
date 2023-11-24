namespace TaskMaster.Model.API.Update;

public class APIUpdateUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public int UserRoleId { get; set; }
}

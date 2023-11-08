namespace TaskMaster.Model.API.Create;

public class APICreateUser
{
    public string FirstName { set; get; }

    public string LastName { set; get; }

    public string Username { set; get; }

    public string Email { set; get; }

    public string Password { set; get; }

    public int UserRoleId { get; set; }
}

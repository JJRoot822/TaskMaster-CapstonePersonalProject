namespace TaskMaster.Model.API;

public class APIUser
{
    public int UserId { set; get; }

    public string FirstName { set; get; }

    public string LastName { set; get; }

    public string Username { set; get; }

    public string Email { set; get; }

    public string Role { get; set; }
    
    public List<APIProject> Projects { get; set; }
}
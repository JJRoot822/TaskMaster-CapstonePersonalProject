namespace TaskMaster.Model.API.CrudOperations.Project;

public class APIUpdateProject
{
    public int ProjectId { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
    public string Description { get; set; }
    public DateTime ReleaseDate { get; set; }
}

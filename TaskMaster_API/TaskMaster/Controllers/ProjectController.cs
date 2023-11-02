using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using TaskMaster.Data.Context;
using TaskMaster.Model.API.ProjectData;
using TaskMaster.Model.Domain.ProjectData;

namespace TaskMaster.Controllers;

[Route("api/project")]
[ApiController]
public class ProjectController : ControllerBase
{
    private readonly TaskMasterDbContext _context;

    public ProjectController(TaskMasterDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    [Route("all")]
    public async Task<List<APIProject>> GetAllProjects()
    {
        List<Project> projects = await _context.Projects.ToListAsync();
        List<APIProject> apiProjects = ToAPIProjectsList(projects);

        return apiProjects;

    }

    [HttpGet("{id}")]
    public async Task<ActionResult<APIProject>> GetProjectById(int id)
    {
        Project project = await _context.Projects.FindAsync(id);

        if (project == null)
        {
            return NotFound();
        }

        APIProject apiProject = new APIProject
        {
            Name = project.Name,
            Color = project.Color,
            Description = project.Description,
            ReleaseDate = project.ReleaseDate,
            UserId = project.UserId
        };

        return Ok(apiProject);
    }

    [HttpPost]
    [Route("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult>  CreateProject([FromBody] APICreateProject apiProject)
    {
        if (apiProject == null)
        {
            return BadRequest();
        }

        if (apiProject.Name == null || apiProject.Name == "" || 
            apiProject.Color == null || apiProject.Color == "" ||
            apiProject.Description == null || apiProject.Description == "" ||
            apiProject.UserId == null || apiProject.UserId < 1 || 
            apiProject.ReleaseDate == null)
        {
            return BadRequest();
            }

        Project project = new Project()
        {
            Name = apiProject.Name,
            Color = apiProject.Color,
            Description = apiProject.Description,
            UserId = apiProject.UserId,
            ReleaseDate = apiProject.ReleaseDate
        };

        _context.Projects.Add(project);

        await _context.SaveChangesAsync();

        return StatusCode(201, project);
    }

    // PUT api/<ProjectController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<ProjectController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }

    private List<APIProject> ToAPIProjectsList(List<Project> projects)
    {
        List<APIProject> apiProjects = new List<APIProject>();

        foreach (var project in projects)
        {
            APIProject apiProject = new APIProject
            {
                ProjectId = project.ProjectId,
                Name = project.Name,
                Color = project.Color,
                Description = project.Description,
                ReleaseDate = project.ReleaseDate,
                UserId = project.UserId
            };

            apiProjects.Add(apiProject);
        }

        return apiProjects;
    }
}

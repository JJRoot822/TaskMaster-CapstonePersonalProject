using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using TaskMaster.Data.Context;
using TaskMaster.Model.API.CrudOperations.Project;
using TaskMaster.Model.API.ProjectData;
using TaskMaster.Model.Domain.ProjectData;
using TaskMaster.Model.Domain.UserData;

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
        var project = await _context.Projects.FindAsync(id);

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
            apiProject.UserId < 1)
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

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProject(int id, [FromBody] APIUpdateProject apiProject)
    {
        if (apiProject.ProjectId != id)
        {
            return BadRequest();
        }

        var project = await _context.Projects.FindAsync(id);

        if (project == null)
        {
            return NotFound();
        }

        project.Name = apiProject.Name;
        project.Color = apiProject.Color;
        project.Description = apiProject.Description;
        project.ReleaseDate = apiProject.ReleaseDate;

        _context.Entry(project).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            bool doesProjectExist = await ProjectExists(id);

            if (!doesProjectExist)
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProject(int id)
    {
        var project = await _context.Projects.FindAsync(id);

        if (project == null)
        {
            return NotFound();
        }

        _context.Projects.Remove(project);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    private async Task<bool> ProjectExists(int id)
    {
        var project = await _context.Projects.FindAsync(id);

        return project != null;
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

using System.ComponentModel;
using System.Diagnostics;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using TaskMaster.Data;
using TaskMaster.Data.Context;
using TaskMaster.Model.API;
using TaskMaster.Model.API.Create;
using TaskMaster.Model.API.Update;
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
    public async Task<List<APIProject>> GetAllProjects()
    {
        List<Project> projects = await _context.Projects.ToListAsync();

        return ModelConverter.ToListOfAPIProjects(_context, projects);
    }

    [HttpGet("}id}")]
    public async Task<ActionResult<APIProject>> GetProjectById(int id)
    {
        var project = await _context.Projects.FindAsync(id);

        if (project == null)
        {
            return NotFound();
        }

        return Ok(ModelConverter.ToAPIProject(_context, project));
    }

    [HttpGet("user/{id}")]
    public async Task<ActionResult<List<APIProject>>> GetProjectsByUser(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        var projects = await _context.Projects.Where(p => p.UserId == id).ToListAsync();

        return Ok(ModelConverter.ToListOfAPIProjects(_context, projects));
    }

    [HttpPost]
    [Route("create")]
    public async Task<ActionResult<APIProject>> CreateProject([FromBody] APICreateProject request)
    {
        var project = new Project();
        project.Name = request.Name;
        project.Color = request.Color;
        project.Description = request.Description;
        project.ReleaseDate = request.ReleaseDate;
        project.UserId = request.UserId;

        await _context.Projects.AddAsync(project);
        await _context.SaveChangesAsync();

        return StatusCode(201, ModelConverter.ToAPIProject(_context, project));
    }

    [HttpPatch("update/{id}")]
    public async Task<IActionResult> UpdateProject(int id, [FromBody] APIUpdateProject request)
    {
        if (id != request.ProjectId)
        {
            return BadRequest();
        }

        var project = await _context.Projects.FindAsync(id);

        if (project == null)
        {
            return NotFound();
        }

        project.Color = request.Color;
        project.Description = request.Description;
        project.ReleaseDate = request.ReleaseDate;

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
                return Conflict();
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        bool doesProjectExist = await ProjectExists(id)
            ;
        if (doesProjectExist)
        {
            return NotFound();
        }

        var project = await _context.Projects.FindAsync(id);

        _context.Projects.Remove(project);

        return NoContent();
    }

    private async Task<bool>UserExists(int id)
    {
        var user = await _context.Users.FindAsync(id);

        return user != null;
    }

    private async Task<bool> ProjectExists(int id)
    {
        var project = await _context.Projects.FindAsync(id);

        return project != null;
    }
}

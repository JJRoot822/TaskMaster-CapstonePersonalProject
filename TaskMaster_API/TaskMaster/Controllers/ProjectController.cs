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
        if (!ProjectExists(id))
        {
            return NotFound();
        }

        var project = await _context.Projects.FindAsync(id);

        return Ok(ModelConverter.ToAPIProject(_context, project));
    }

    [HttpGet("user/{id}")]
    public async Task<ActionResult<List<APIProject>>> GetProjectsByUser(int id)
    {
        if (!UserExists(id))
        {
            return NotFound();
        }

        var projects = await _context.Projects.Where(p => p.UserId).ToListAsync();

        return Ok(projects);
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

        await _contest.Projects.AddAsync(project);
        await _context.SaveChangesAsync();

        return StatusCode(201, ModelConverter.ToAPIProject(_context, project);
    }

    [HttpPatch("update/{id}")]
    public async Task<IActionResult> UpdateProject(int id, [FromBody] APIUpdateProject request)
    {
        if (id != request.ProjectId)
        {
            return BadRequest();
        }

        if (!ProjectExists(id))
        {
            return NotFound();
        }

        var project = await _context.Projects.FindAsync(request.ProjectId);
        project.Name = request.Name;
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
            if (!ProjectExists(id))
            {
                return NotFound()
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
        if (!ProjectExists(id))
        {
            return NotFound();
        }

        var project = await _context.Projects.FindAsync(id);

        _context.Projects.Remove(project);

        return NoContent();
    }

    private async boolUserExists(int id)
    {
        var user = await _context.Users.FindAsync(id);

        return user != null;
    }

    private async bool ProjectExists(int id)
    {
        var project = await _context.Projects.FindAsync(id);

        return project != null;
    }
}

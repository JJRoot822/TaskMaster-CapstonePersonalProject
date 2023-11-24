using System.ComponentModel;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using TaskMaster.Data;
using TaskMaster.Data.Context;
using TaskMaster.Model.API;
using TaskMaster.Model.API.Create;
using TaskMaster.Model.API.Update;
using TaskMaster.Model.Domain.ProjectData.TaskitemData;

namespace TaskMaster.Controllers;

[Route("api/task")]
[ApiController]
public class TaskItemController : ControllerBase
{
    private readonly TaskMasterDbContext _context;

    public TaskItemController(TaskMasterDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("all")]
    public async Task<List<APITaskItem>> GetAllTaskItems()
    {
        List<TaskItem> tasks = await _context.TaskItems.ToListAsync();

        return ModelConverter.ToListOfAPITaskItems(tasks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<APITaskItem>> GetTaskItemById(int id)
    {
        var task = await _context.TaskItems.FindAsync(id);

        if (task == null)
        {
            return NotFound();
        }

        return Ok(ModelConverter.ToAPITaskItem(task));
    }

    [HttpGet("project/{id}")]
    public async Task<ActionResult<List<APITaskItem>>> GetAllTasksByProjectId(int id)
    {
        var project = await _context.Projects.FindAsync(id);

        if (project == null)
        {
            return NotFound();
        }

        var tasks = await _context.TaskItems.Where(
            taskItem => taskItem.ProjectId == id
        ).ToListAsync();

        return Ok(ModelConverter.ToListOfAPITaskItems(tasks));
    }

    [HttpPost]
    public async Task<ActionResult<APITaskItem>> CreateTaskItem([FromBody] APICreateTaskItem request)
    {
        var task = new TaskItem();
        task.Title = request.Title;
        task.Priority = request.Priority;
        task.Completed= request.Completed;
        task.Notes = request.Notes;
        task.Details = request.Details;
        task.CompletedDate = request.CompletedDate;
        task.DueDate = request.DueDate;
        task.ProjectId = request.ProjectId;
        task.UserId = request.UserId;

        await _context.TaskItems.AddAsync(task);
        await _context.SaveChangesAsync();

        return StatusCode(201, ModelConverter.ToAPITaskItem(task));

    }
    [HttpPatch("update/{id}")]
    public async Task<IActionResult> UpdateTaskItem(int id, [FromBody] APIUpdateTaskItem request)
    {
        if (id != request.taskItemId)
        {
            return BadRequest();
        }

        var task = await _context.TaskItems.FindAsync(id);

        if (task == null)
        {
            return NotFound();
        }

        task.Title = request.Title;
        task.DueDate = request.DueDate;
        task.CompletedDate = request.CompletedDate;
        task.Completed = request.Completed;
        task.Details = request.Details;
        task.Notes = request.Notes;
        task.Priority = request.Priority;

        _context.Entry(task).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {

            bool doesTaskItemExist = await TaskItemExists(id);

            if (!doesTaskItemExist)
            {
                return BadRequest();
            }
            else
            {
                return Conflict();
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTaskItem(int id)
    {
        var task = await _context.TaskItems.FindAsync(id);

        if (task == null)
        {
            return NotFound();
        }

        _context.TaskItems.Remove(task);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    private async Task<bool> TaskItemExists(int id)
    {
        var task = await _context.TaskItems.FindAsync(id);

        return task != null;
    }
}

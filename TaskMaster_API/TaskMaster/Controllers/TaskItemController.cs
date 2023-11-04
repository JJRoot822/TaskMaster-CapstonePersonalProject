using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using TaskMaster.Data.Context;
using TaskMaster.Model.API.CrudOperations.Task;
using TaskMaster.Model.API.ProjectData.TaskItemData;
using TaskMaster.Model.Domain.ProjectData.TaskitemData;

namespace TaskMaster.Controllers;

[Route("api/taskitem")]
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
        List<TaskItem> taskItems = await _context.TaskItems.ToListAsync();
        List<APITaskItem> apiTaskItems = ToAPITaskItemsList(taskItems);

        return apiTaskItems;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<APITaskItem>> GetTaskItemById(int id)
    {
        TaskItem taskItem = await _context.TaskItems.FindAsync(id);

        if (taskItem == null)
        {
            return NotFound();
        }

        APITaskItem apiTaskItem = new APITaskItem
        {
            TaskItemId = taskItem.TaskItemId,
            Title = taskItem.Title,
            Priority = taskItem.Priority,
            Completed = taskItem.Completed,
            Notes = taskItem.Notes,
            Details = taskItem.Details,
            CompletedDate = taskItem.CompletedDate,
            DueDate = taskItem.DueDate,
            ProjectId = taskItem.ProjectId,
            UserId = taskItem.UserId
        };

        return Ok(apiTaskItem);
    }

    [HttpPost]
    [Route("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTaskItem([FromBody] APICreateTaskItem apiTaskItem)
    {
        if (apiTaskItem == null)
        {
            return BadRequest();
        }

        // Validation can be added for the input values similar to the ProjectController

        TaskItem taskItem = new TaskItem()
        {
            Title = apiTaskItem.Title,
            Priority = apiTaskItem.Priority,
            Completed = apiTaskItem.Completed,
            Notes = apiTaskItem.Notes,
            Details = apiTaskItem.Details,
            DueDate = apiTaskItem.DueDate,
            CompletedDate = apiTaskItem.CompletedDate,
            ProjectId = apiTaskItem.ProjectId,
            UserId = apiTaskItem.UserId
        };

        _context.TaskItems.Add(taskItem);
        await _context.SaveChangesAsync();

        return StatusCode(201, taskItem);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateTaskItem(int id, [FromBody] APIUpdateTaskItem apiTaskItem)
    {
        if (apiTaskItem.TaskItemId != id)
        {
            return BadRequest();
        }

        var taskItem = await _context.TaskItems.FindAsync(id);

        if (taskItem == null)
        {
            return NotFound();
        }

        taskItem.Title = apiTaskItem.Title;
        taskItem.Priority = apiTaskItem.Priority;
        taskItem.Completed = apiTaskItem.Completed;
        taskItem.Notes = apiTaskItem.Notes;
        taskItem.Details = apiTaskItem.Details;
        taskItem.DueDate = apiTaskItem.DueDate;
        taskItem.CompletedDate = apiTaskItem.CompletedDate;

        _context.Entry(taskItem).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            bool doesTaskItemExist = await TaskItemExists(id);

            if (!doesTaskItemExist)
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
    public async Task<IActionResult> DeleteTaskItem(int id)
    {
        var taskItem = await _context.TaskItems.FindAsync(id);

        if (taskItem == null)
        {
            return NotFound();
        }

        _context.TaskItems.Remove(taskItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private async Task<bool> TaskItemExists(int id)
    {
        var taskItem = await _context.TaskItems.FindAsync(id);
        return taskItem != null;
    }

    private List<APITaskItem> ToAPITaskItemsList(List<TaskItem> taskItems)
    {
        List<APITaskItem> apiTaskItems = new List<APITaskItem>();

        foreach (var taskItem in taskItems)
        {
            APITaskItem apiTaskItem = new APITaskItem
            {
                TaskItemId = taskItem.TaskItemId,
                Title = taskItem.Title,
                Priority = taskItem.Priority,
                Completed = taskItem.Completed,
                Notes = taskItem.Notes,
                Details = taskItem.Details,
                CompletedDate = taskItem.CompletedDate,
                DueDate = taskItem.DueDate,
                ProjectId = taskItem.ProjectId,
                UserId = taskItem.UserId
            };

            apiTaskItems.Add(apiTaskItem);
        }

        return apiTaskItems;
    }
}

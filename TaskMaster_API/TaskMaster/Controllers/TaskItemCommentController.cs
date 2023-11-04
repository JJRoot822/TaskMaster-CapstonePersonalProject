using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using TaskMaster.Data.Context;
using TaskMaster.Model.API.CrudOperations.TaskItemComment;
using TaskMaster.Model.API.ProjectData.TaskItemData;
using TaskMaster.Model.Domain.ProjectData.TaskitemData;

namespace TaskMaster.Controllers;

[Route("api/comment")]
[ApiController]
public class TaskItemCommentController : ControllerBase
{
    private readonly TaskMasterDbContext _context;

    public TaskItemCommentController(TaskMasterDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("all")]
    public async Task<List<APITaskItemComment>> GetAllTaskItemComments()
    {
        List<TaskItemComment> comments = await _context.TaskItemComments.ToListAsync();
        List<APITaskItemComment> apiComments = ToAPITaskItemCommentsList(comments);

        return apiComments;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<APITaskItemComment>> GetTaskItemCommentById(int id)
    {
        TaskItemComment comment = await _context.TaskItemComments.FindAsync(id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

        if (comment == null)
        {
            return NotFound();
        }

        APITaskItemComment apiComment = new APITaskItemComment
        {
            TaskCommentId = comment.TaskCommentId,
            Content = comment.Content,
            DatePosted = comment.DatePosted,
            TaskItemId = comment.TaskItemId,
            UserId = comment.UserId
        };

        return Ok(apiComment);
    }

    [HttpPost]
    [Route("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTaskItemComment([FromBody] APICreateTaskItemComment apiComment)
    {
        if (apiComment == null)
        {
            return BadRequest();
        }

        // Validation can be added for the input values similar to the ProjectController

        TaskItemComment comment = new TaskItemComment()
        {
            Content = apiComment.Content,
            DatePosted = apiComment.DatePosted,
            TaskItemId = apiComment.TaskItemId,
            UserId = apiComment.UserId
        };

        _context.TaskItemComments.Add(comment);
        await _context.SaveChangesAsync();

        return StatusCode(201, comment);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateTaskItemComment(int id, [FromBody] APIUpdateTaskItemComment apiComment)
    {
        if (apiComment.TaskCommentId != id)
        {
            return BadRequest();
        }

        var comment = await _context.TaskItemComments.FindAsync(id);

        if (comment == null)
        {
            return NotFound();
        }

        comment.Content = apiComment.Content;

        _context.Entry(comment).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            bool doesCommentExist = await TaskItemCommentExists(id);

            if (!doesCommentExist)
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
    public async Task<IActionResult> DeleteTaskItemComment(int id)
    {
        var comment = await _context.TaskItemComments.FindAsync(id);

        if (comment == null)
        {
            return NotFound();
        }

        _context.TaskItemComments.Remove(comment);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private async Task<bool> TaskItemCommentExists(int id)
    {
        var comment = await _context.TaskItemComments.FindAsync(id);
        return comment != null;
    }

    private List<APITaskItemComment> ToAPITaskItemCommentsList(List<TaskItemComment> comments)
    {
        List<APITaskItemComment> apiComments = new List<APITaskItemComment>();

        foreach (var comment in comments)
        {
            APITaskItemComment apiComment = new APITaskItemComment
            {
                TaskCommentId = comment.TaskCommentId,
                Content = comment.Content,
                DatePosted = comment.DatePosted,
                TaskItemId = comment.TaskItemId,
                UserId = comment.UserId
            };

            apiComments.Add(apiComment);
        }

        return apiComments;
    }
}

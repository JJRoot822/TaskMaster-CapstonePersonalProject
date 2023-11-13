using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using TaskMaster.Data;
using TaskMaster.Data.Context;
using TaskMaster.Model.API;
using TaskMaster.Model.API.Create;
using TaskMaster.Model.API.Update;
using TaskMaster.Model.Domain.ProjectData;

namespace TaskMaster.Controllers;

[Route("api/issues")]
[ApiController]
public class IssueReportController : ControllerBase
{
    private readonly TaskMasterDbContext _context;

    public IssueReportController(TaskMasterDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<List<APIIssueReport>> GetAllIssueReports()
    {
        List<IssueReport> issueReports = await _context.IssueReports.ToListAsync();
        return ModelConverter.ToListOfAPIIssueReports(issueReports);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<APIIssueReport>> GetIssueReportById(int id)
    {
        var issueReport = await _context.IssueReports.FindAsync(id);

        if (issueReport == null)
        {
            return NotFound();
        }

        return Ok(ModelConverter.ToAPIIssueReport(issueReport));
    }

    [HttpPost]
    public async Task<ActionResult<APIIssueReport>> CreateIssueReport([FromBody] APICreateIssueReport request)
    {
        var issueReport = new IssueReport
        {
            Title = request.Title,
            Priority = request.Priority,
            Severity = request.Severity,
            IssueType = request.IssueType,
            Details = request.Details,
            Fixed = request.Fixed,
            DateResolved = request.DateResolved,
            Notes = request.Notes,
            UserId = request.UserId,
            ProjectId = request.ProjectId
        };

        await _context.IssueReports.AddAsync(issueReport);
        await _context.SaveChangesAsync();

        return StatusCode(201, ModelConverter.ToAPIIssueReport(issueReport));
    }

    [HttpPatch("update/{id}")]
    public async Task<IActionResult> UpdateIssueReport(int id, [FromBody] APIUpdateIssueReport request)
    {
        if (id != request.IssueReportId)
        {
            return BadRequest();
        }

        var issueReport = await _context.IssueReports.FindAsync(id);

        if (issueReport == null)
        {
            return NotFound();
        }

        issueReport.Title = request.Title;
        issueReport.Priority = request.Priority;
        issueReport.Severity = request.Severity;
        issueReport.IssueType = request.IssueType;
        issueReport.Details = request.Details;
        issueReport.Fixed = request.Fixed;
        issueReport.DateResolved = request.DateResolved;
        issueReport.Notes = request.Notes;

        _context.Entry(issueReport).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            bool doesIssueReportExist = await IssueReportExists(id);

            if (!doesIssueReportExist)
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
    public async Task<IActionResult> DeleteIssueReport(int id)
    {
        var issueReport = await _context.IssueReports.FindAsync(id);

        if (issueReport == null)
        {
            return NotFound();
        }

        _context.IssueReports.Remove(issueReport);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private async Task<bool> IssueReportExists(int id)
    {
        var issueReport = await _context.IssueReports.FindAsync(id);
        return issueReport != null;
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using TaskMaster.Data;
using TaskMaster.Data.Context;
using TaskMaster.Model.API;
using TaskMaster.Model.API.Create;
using TaskMaster.Model.API.Update;
using TaskMaster.Model.Domain.ProjectData;

namespace TaskMaster.Controllers;

[Route("api/bugs")]
[ApiController]
public class BugReportController : ControllerBase
{
    private readonly TaskMasterDbContext _context;

    public BugReportController(TaskMasterDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("all")]
    public async Task<List<APIBugReport>> GetAllBugReports()
    {
        List<BugReport> bugReports = await _context.BugReports.ToListAsync();

        return ModelConverter.ToListOfAPIBugReports(bugReports);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<APIBugReport>> GetBugReportById(int id)
    {
        var bugReport = await _context.BugReports.FindAsync(id);

        if (bugReport == null)
        {
            return NotFound();
        }

        return Ok(ModelConverter.ToAPIBugReport(bugReport));
    }

    [HttpPost]
    public async Task<ActionResult<APIBugReport>> CreateBugReport([FromBody] APICreateBugReport request)
    {
        var bugReport = new BugReport
        {
            Title = request.Title,
            Details = request.Details,
            Notes = request.Notes,
            Priority = request.Priority,
            Severity = request.Severity,
            Fixed = request.Fixed,
            DateFixed = request.DateFixed,
            UserId = request.UserId,
            ProjectId = request.ProjectId
        };

        await _context.BugReports.AddAsync(bugReport);
        await _context.SaveChangesAsync();

        return StatusCode(201, ModelConverter.ToAPIBugReport(bugReport));
    }

    [HttpPatch("update/{id}")]
    public async Task<IActionResult> UpdateBugReport(int id, [FromBody] APIUpdateBugReport request)
    {
        if (id != request.BugReportId)
        {
            return BadRequest();
        }

        var bugReport = await _context.BugReports.FindAsync(id);

        if (bugReport == null)
        {
            return NotFound();
        }

        bugReport.Title = request.Title;
        bugReport.Details = request.Details;
        bugReport.Notes = request.Notes;
        bugReport.Priority = request.Priority;
        bugReport.Severity = request.Severity;
        bugReport.Fixed = request.Fixed;
        bugReport.DateFixed = request.DateFixed;

        _context.Entry(bugReport).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            bool doesBugReportExist = await BugReportExists(id);

            if (!doesBugReportExist)
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
    public async Task<IActionResult> DeleteBugReport(int id)
    {
        var bugReport = await _context.BugReports.FindAsync(id);

        if (bugReport == null)
        {
            return NotFound();
        }

        _context.BugReports.Remove(bugReport);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    private async Task<bool> BugReportExists(int id)
    {
        var bugReport = await _context.BugReports.FindAsync(id);

        return bugReport != null;
    }
}

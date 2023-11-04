using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using TaskMaster.Data.Context;
using TaskMaster.Model.API.CrudOperations.BugReport;
using TaskMaster.Model.API.ProjectData;
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
        List<APIBugReport> apiBugReports = ToAPIBugReportsList(bugReports);
        return apiBugReports;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<APIBugReport>> GetBugReportById(int id)
    {
        BugReport bugReport = await _context.BugReports.FindAsync(id);

        if (bugReport == null)
        {
            return NotFound();
        }

        APIBugReport apiBugReport = new APIBugReport
        {
            BugReportId = bugReport.BugReportId,
            Title = bugReport.Title,
            Details = bugReport.Details,
            Notes = bugReport.Notes,
            Priority = bugReport.Priority,
            Severity = bugReport.Severity,
            Fixed = bugReport.Fixed,
            DateFixed = bugReport.DateFixed,
            UserId = bugReport.UserId,
            ProjectId = bugReport.ProjectId
        };

        return Ok(apiBugReport);
    }

    [HttpPost]
    [Route("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateBugReport([FromBody] APICreateBugReport apiBugReport)
    {
        if (apiBugReport == null)
        {
            return BadRequest();
        }

        // Validation can be added for the input values similar to other controllers

        BugReport bugReport = new BugReport()
        {
            Title = apiBugReport.Title,
            Details = apiBugReport.Details,
            Notes = apiBugReport.Notes,
            Priority = apiBugReport.Priority,
            Severity = apiBugReport.Severity,
            Fixed = apiBugReport.Fixed,
            DateFixed = apiBugReport.DateFixed,
            UserId = apiBugReport.UserId,
            ProjectId = apiBugReport.ProjectId
        };

        _context.BugReports.Add(bugReport);
        await _context.SaveChangesAsync();

        return StatusCode(201, bugReport);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateBugReport(int id, [FromBody] APIUpdateBugReport apiBugReport)
    {
        if (apiBugReport.BugReportId != id)
        {
            return BadRequest();
        }

        var bugReport = await _context.BugReports.FindAsync(id);

        if (bugReport == null)
        {
            return NotFound();
        }

        bugReport.Title = apiBugReport.Title;
        bugReport.Details = apiBugReport.Details;
        bugReport.Notes = apiBugReport.Notes;
        bugReport.Priority = apiBugReport.Priority;
        bugReport.Severity = apiBugReport.Severity;
        bugReport.Fixed = apiBugReport.Fixed;
        bugReport.DateFixed = apiBugReport.DateFixed;

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

    private List<APIBugReport> ToAPIBugReportsList(List<BugReport> bugReports)
    {
        List<APIBugReport> apiBugReports = new List<APIBugReport>();

        foreach (var bugReport in bugReports)
        {
            APIBugReport apiBugReport = new APIBugReport
            {
                BugReportId = bugReport.BugReportId,
                Title = bugReport.Title,
                Details = bugReport.Details,
                Notes = bugReport.Notes,
                Priority = bugReport.Priority,
                Severity = bugReport.Severity,
                Fixed = bugReport.Fixed,
                DateFixed = bugReport.DateFixed,
                UserId = bugReport.UserId,
                ProjectId = bugReport.ProjectId
            };

            apiBugReports.Add(apiBugReport);
        }

        return apiBugReports;
    }
}

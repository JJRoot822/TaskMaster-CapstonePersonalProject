using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using TaskMaster.Data.Context;
using TaskMaster.Model.API.CrudOperations.IssueReport;
using TaskMaster.Model.API.ProjectData;
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
    [Route("all")]
    public async Task<List<APIIssueReport>> GetAllIssueReports()
    {
        List<IssueReport> issueReports = await _context.IssueReports.ToListAsync();
        List<APIIssueReport> apiIssueReports = ToAPIIssueReportsList(issueReports);
        return apiIssueReports;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<APIIssueReport>> GetIssueReportById(int id)
    {
        IssueReport issueReport = await _context.IssueReports.FindAsync(id);

        if (issueReport == null)
        {
            return NotFound();
        }

        APIIssueReport apiIssueReport = new APIIssueReport
        {
            IssueReportId = issueReport.IssueReportId,
            Title = issueReport.Title,
            Priority = issueReport.Priority,
            Severity = issueReport.Severity,
            IssueType = issueReport.IssueType,
            Details = issueReport.Details,
            Fixed = issueReport.Fixed,
            DateResolved = issueReport.DateResolved,
            Notes = issueReport.Notes,
            UserId = issueReport.UserId,
            ProjectId = issueReport.ProjectId
        };

        return Ok(apiIssueReport);
    }

    [HttpPost]
    [Route("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateIssueReport([FromBody] APICreateIssueReport apiIssueReport)
    {
        if (apiIssueReport == null)
        {
            return BadRequest();
        }

        // Add validation for input values

        IssueReport issueReport = new IssueReport()
        {
            Title = apiIssueReport.Title,
            Priority = apiIssueReport.Priority,
            Severity = apiIssueReport.Severity,
            IssueType = apiIssueReport.IssueType,
            Details = apiIssueReport.Details,
            Fixed = apiIssueReport.Fixed,
            DateResolved = apiIssueReport.DateResolved,
            Notes = apiIssueReport.Notes,
            UserId = apiIssueReport.UserId,
            ProjectId = apiIssueReport.ProjectId
        };

        _context.IssueReports.Add(issueReport);
        await _context.SaveChangesAsync();

        return StatusCode(201, issueReport);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateIssueReport(int id, [FromBody] APIUpdateIssueReport apiIssueReport)
    {
        if (apiIssueReport.IssueReportId != id)
        {
            return BadRequest();
        }

        var issueReport = await _context.IssueReports.FindAsync(id);

        if (issueReport == null)
        {
            return NotFound();
        }

        issueReport.Title = apiIssueReport.Title;
        issueReport.Priority = apiIssueReport.Priority;
        issueReport.Severity = apiIssueReport.Severity;
        issueReport.IssueType = apiIssueReport.IssueType;
        issueReport.Details = apiIssueReport.Details;
        issueReport.Fixed = apiIssueReport.Fixed;
        issueReport.DateResolved = apiIssueReport.DateResolved;
        issueReport.Notes = apiIssueReport.Notes;

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

    private List<APIIssueReport> ToAPIIssueReportsList(List<IssueReport> issueReports)
    {
        List<APIIssueReport> apiIssueReports = new List<APIIssueReport>();

        foreach (var issueReport in issueReports)
        {
            APIIssueReport apiIssueReport = new APIIssueReport
            {
                IssueReportId = issueReport.IssueReportId,
                Title = issueReport.Title,
                Priority = issueReport.Priority,
                Severity = issueReport.Severity,
                IssueType = issueReport.IssueType,
                Details = issueReport.Details,
                Fixed = issueReport.Fixed,
                DateResolved = issueReport.DateResolved,
                Notes = issueReport.Notes,
                UserId = issueReport.UserId,
                ProjectId = issueReport.ProjectId
            };

            apiIssueReports.Add(apiIssueReport);
        }

        return apiIssueReports;
    }
}

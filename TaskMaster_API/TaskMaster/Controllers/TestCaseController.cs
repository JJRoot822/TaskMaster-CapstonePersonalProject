using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using TaskMaster.Data.Context;
using TaskMaster.Model.API.CrudOperations.TestCase;
using TaskMaster.Model.API.ProjectData;
using TaskMaster.Model.Domain.ProjectData;
using TaskMaster.Model.Domain.ProjectData;

namespace TaskMaster.Controllers;

[Route("api/testcase")]
[ApiController]
public class TestCaseController : ControllerBase
{
    private readonly TaskMasterDbContext _context;

    public TestCaseController(TaskMasterDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("all")]
    public async Task<List<APITestCase>> GetAllTestCases()
    {
        List<TestCase> testCases = await _context.TestCases.ToListAsync();
        List<APITestCase> apiTestCases = ToAPITestCasesList(testCases);
        return apiTestCases;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<APITestCase>> GetTestCaseById(int id)
    {
        TestCase testCase = await _context.TestCases.FindAsync(id);

        if (testCase == null)
        {
            return NotFound();
        }

        APITestCase apiTestCase = new APITestCase
        {
            TestCaseId = testCase.TestCaseId,
            Title = testCase.Title,
            Details = testCase.Details,
            UserId = testCase.UserId,
            ProjectId = testCase.ProjectId
        };

        return Ok(apiTestCase);
    }

    [HttpPost]
    [Route("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTestCase([FromBody] APICreateTestCase apiTestCase)
    {
        if (apiTestCase == null)
        {
            return BadRequest();
        }

        // Validation can be added for the input values similar to the ProjectController

        TestCase testCase = new TestCase()
        {
            Title = apiTestCase.Title,
            Details = apiTestCase.Details,
            UserId = apiTestCase.UserId,
            ProjectId = apiTestCase.ProjectId
        };

        _context.TestCases.Add(testCase);
        await _context.SaveChangesAsync();

        return StatusCode(201, testCase);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateTestCase(int id, [FromBody] APIUpdateTestCase apiTestCase)
    {
        if (apiTestCase.TestCaseId != id)
        {
            return BadRequest();
        }

        var testCase = await _context.TestCases.FindAsync(id);

        if (testCase == null)
        {
            return NotFound();
        }

        testCase.Title = apiTestCase.Title;
        testCase.Details = apiTestCase.Details;

        _context.Entry(testCase).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            bool doesTestCaseExist = await TestCaseExists(id);

            if (!doesTestCaseExist)
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
    public async Task<IActionResult> DeleteTestCase(int id)
    {
        var testCase = await _context.TestCases.FindAsync(id);

        if (testCase == null)
        {
            return NotFound();
        }

        _context.TestCases.Remove(testCase);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private async Task<bool> TestCaseExists(int id)
    {
        var testCase = await _context.TestCases.FindAsync(id);
        return testCase != null;
    }

    private List<APITestCase> ToAPITestCasesList(List<TestCase> testCases)
    {
        List<APITestCase> apiTestCases = new List<APITestCase>();

        foreach (var testCase in testCases)
        {
            APITestCase apiTestCase = new APITestCase
            {
                TestCaseId = testCase.TestCaseId,
                Title = testCase.Title,
                Details = testCase.Details,
                UserId = testCase.UserId,
                ProjectId = testCase.ProjectId
            };

            apiTestCases.Add(apiTestCase);
        }

        return apiTestCases;
    }
}

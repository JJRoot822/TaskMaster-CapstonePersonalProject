using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using TaskMaster.Data;
using TaskMaster.Data.Context;
using TaskMaster.Model.API;
using TaskMaster.Model.API.Create;
using TaskMaster.Model.API.Update;
using TaskMaster.Model.Domain.ProjectData;

namespace TaskMaster.Controllers;

[Route("api/testcases")]
[ApiController]
public class TestCaseController : ControllerBase
{
    private readonly TaskMasterDbContext _context;

    public TestCaseController(TaskMasterDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<List<APITestCase>> GetAllTestCases()
    {
        List<TestCase> testCases = await _context.TestCases.ToListAsync();
        return ModelConverter.ToListOfAPITestCases(testCases);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<APITestCase>> GetTestCaseById(int id)
    {
        var testCase = await _context.TestCases.FindAsync(id);

        if (testCase == null)
        {
            return NotFound();
        }

        return Ok(ModelConverter.ToAPITestCase(testCase));
    }

    [HttpPost]
    public async Task<ActionResult<APITestCase>> CreateTestCase([FromBody] APICreateTestCase request)
    {
        var testCase = new TestCase
        {
            Title = request.Title,
            Details = request.Details,
            UserId = request.UserId,
            ProjectId = request.ProjectId
        };

        await _context.TestCases.AddAsync(testCase);
        await _context.SaveChangesAsync();

        return StatusCode(201, ModelConverter.ToAPITestCase(testCase));
    }

    [HttpPatch("update/{id}")]
    public async Task<IActionResult> UpdateTestCase(int id, [FromBody] APIUpdateTestCase request)
    {
        if (id != request.TestCaseId)
        {
            return BadRequest();
        }

        var testCase = await _context.TestCases.FindAsync(id);

        if (testCase == null)
        {
            return NotFound();
        }

        testCase.Title = request.Title;
        testCase.Details = request.Details;

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
}

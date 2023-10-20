using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using TaskMaster.Data.Context;
using TaskMaster.Model;

namespace TaskMaster.Controllers;

[ApiController]
[Route("api")]
public class AuthenticationController : ControllerBase
{
    private readonly ILogger<AuthenticationController> _logger;
    private readonly TaskMasterDbContext _context;

    public AuthenticationController(ILogger<AuthenticationController> logger, TaskMasterDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpPost]
    [Route("authenticate")]
    public async Task<ActionResult<User>> Authenticate([FromBody] AuthenticationRequest request)
    {

    }
}

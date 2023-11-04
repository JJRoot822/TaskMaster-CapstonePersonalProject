using System.Text;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using TaskMaster.Data.Context;
using TaskMaster.Model.API.Authentication;
using TaskMaster.Model.API.UserData;
using TaskMaster.Model.Domain.UserData;
using TaskMaster.Services.Security;

namespace TaskMaster.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthenticationController : ControllerBase
{
    private readonly TaskMasterDbContext _context;

    public AuthenticationController(TaskMasterDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Route("auth")]
    public async Task<ActionResult<APIUser>> Authenticate([FromBody] AuthenticationRequest request)
    {
        User? user = await _context.Users.FirstOrDefaultAsync(
            u => u.Email == request.Email && u.Username == request.Username
        );

        if (user == null)
        {
            return NotFound();
        }

        if (!SecurityService.DoPasswordsMatch(request.Password, user.Password))
        {
            return Unauthorized("The password you provided is invalid.");
        }

        APIUser apiUser = new APIUser();
        apiUser.FirstName = user.FirstName;
        apiUser.LastName = user.LastName;
        apiUser.Username = user.Username;
        apiUser.Email = user.Email;
        apiUser.UserRoleId = user.UserRoleId;

        return Ok(apiUser);
    }
}

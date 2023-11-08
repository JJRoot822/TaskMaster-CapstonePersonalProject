using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using TaskMaster.Data;
using TaskMaster.Data.Context;
using TaskMaster.Model.API;
using TaskMaster.Model.API.Create;
using TaskMaster.Model.API.Update;
using TaskMaster.Model.Domain.UserData;
using TaskMaster.Services.Security;

namespace TaskMaster.Controllers;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly TaskMasterDbContext _context;

    public UserController(TaskMasterDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<List<APIUser>> GetAllUsers()
    {
        List<User> users = await _context.Users.ToListAsync();
        return ModelConverter.ToListOfAPIUsers(_context, users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<APIUser>> GetUserById(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(ModelConverter.ToAPIUser(_context, user));
    }

    [HttpGet]
    public async Task<ActionResult<APIUser>> GetUserByUsername(string username)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(ModelConverter.ToAPIUser(_context, user));
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] APICreateUser request)
    {
        if (request == null)
        {
            return BadRequest();
        }

        if (!Validator.IsNotEmpty(request.FirstName) || 
            !Validator.IsNotEmpty(request.LastName) || 
            !Validator.IsValidEmail(request.Email) || 
            !Validator.IsValidUsername(request.Username) || 
            !Validator.IsValidPassword(request.Password))
        {
            return BadRequest();
        }

        User user = new User();
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Email = request.Email;
        user.Username = request.Username;
        user.Password = SecurityService.HashPassword(request.Password);
        user.UserRoleId = request.UserRoleId;

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return StatusCode(201, ModelConverter.ToAPIUser(_context, user));
    }

    [HttpPatch("update-password/{id}")]
    public async Task<IActionResult> UpdatePassword(int id, [FromBody] APIUpdatePassword request)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        user.Password = SecurityService.HashPassword(request.Password);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    public async Task<IActionResult> UpdateUser(int id, [FromBody] APIUpdateUser request)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Username = request.Username;
        user.Email = request.Email;
        user.UserRoleId = request.UserRoleId;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        _context.Users.Remove(user);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}

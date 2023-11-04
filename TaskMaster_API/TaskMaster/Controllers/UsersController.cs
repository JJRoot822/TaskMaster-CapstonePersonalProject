using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using TaskMaster.Data.Context;
using TaskMaster.Model.API.CrudOperations.User;
using TaskMaster.Model.API.UserData;
using TaskMaster.Model.Domain.UserData;
using TaskMaster.Security;

namespace TaskMaster.Controllers;

[Route("api/user")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly TaskMasterDbContext _context;

    public UsersController(TaskMasterDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<List<User>> GetAllUsers() => await _context.Users.ToListAsync();

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIUser))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<User>> GetUserById(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost]
    [Route("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUser([FromBody] APICreateUser apiUser)
    {
        if (apiUser == null)
        {
            return BadRequest();
        }
        if (apiUser.FirstName == null || 
            apiUser.FirstName == "" || 
            apiUser.LastName == null || 
            apiUser.LastName == "" || 
            apiUser.Username == null || apiUser.Username == "" || 
            apiUser.Email == null || 
            apiUser.Email == "" || 
            apiUser.UserRoleId == null ||
            apiUser.UserRoleId < 1)
        {
            return BadRequest();
        }

        User user = new User
        {
            FirstName = apiUser.FirstName,
            LastName = apiUser.LastName,
            Email = apiUser.Email,
            Username = apiUser.Username,
            Password = SecurityUtil.HashPassword(apiUser.Password),
            UserRoleId = apiUser.UserRoleId
        };

        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return StatusCode(201, user);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] APIUpdateUser apiUser)
    {
        if (apiUser.UserId!= id)
        {
            return BadRequest();
        }

        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        user.FirstName = apiUser.FirstName;
        user.LastName = apiUser.LastName;
        user.Username = apiUser.Username;
        user.Email = apiUser.Email;
        
        if (apiUser.Password != null || apiUser.Password != "")
        {
            user.Password = SecurityUtil.HashPassword(apiUser.Password);
        }
        user.UserRoleId = apiUser.UserRoleId;

        _context.Entry(user).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            bool doesUserExist = await UserExists(apiUser.UserId);

            if (!doesUserExist)
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

    private async Task<bool> UserExists(int id)
    {
        var user = await _context.Users.FindAsync(id);

        return user != null;
    }
}

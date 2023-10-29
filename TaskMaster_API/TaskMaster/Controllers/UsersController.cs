using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using TaskMaster.Data.Context;
using TaskMaster.Model.API.User;
using TaskMaster.Model.Domain.User;
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
    public async Task<List<APIUser>> GetAllUsers() =>await _context.Users.ToListAsync()

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIUser))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<APIUser> GetUserById(int id)
    {
        var user = await _context.Users.FindAsync(id);

        return user == null ? NotFound() : Ok(user);
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
        if (apiUser.FirstName.IsNullOrEmpty || 
            apiUser.LastName.IsNullOrEmpty || 
            apiUser.Username.IsNullOrEmpty || 
            apiUser.Email.IsNullOrEmpty || 
            apiUser.Role == null)
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
            Role = apiUser.Role
        };

        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById_IActionResult), new { id = userUserIdd }, user);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] APIUpdateUser apiUser)
    {
        if (apiUser.UserId)
        {
            return BadRequest();
        }

        var user = await _context.Users.FindAsync(id);

        if user == null)
        {
            return NotFound();
        }

        user.FirstName = apiUser.FirstName;
        user.LastName = apiUser.LastName;
        user.Username = apiUser.Username;
        user.Email = apiUser.Email;
        
        if (!apiUser.Password.IsNullOrEmpty)
        {
            user.Password = SecurityUtil.HashPassword(apiUser.Password);
        }
        user.Role = apiUser.Role;

        _context.Entry(user).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(apiUser.UserId))
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

    private async bool UserExists(int id)
    {
        var user = await _context.Users.FindAsync(id);

        return user != null;
    }
}

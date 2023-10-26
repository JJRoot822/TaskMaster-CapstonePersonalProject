using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using TaskMaster.Data.Context;
using TaskMaster.Model.API.User;
using TaskMaster.Model.Domain;

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
            Password = apiUser.Password,
            Role = apiUser.Role
        };

        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById_IActionResult), new { id = userUserIdd }, user);
    }

    // PUT api/<UsersController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<UsersController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}

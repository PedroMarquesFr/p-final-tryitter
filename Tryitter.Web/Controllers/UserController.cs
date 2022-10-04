using Microsoft.AspNetCore.Mvc;
using Tryitter.Web.Models;
using Tryitter.Web.Repository;
using Tryitter.Web.Services;

namespace Tryitter.Web.Controllers;

[ApiController]
[Route("user")]
public class UserController : Controller
{
    private readonly IUserService _service;
    public UserController(IUserService service)
    {
        _service = service;
    }
    [HttpPost("Authentication")]
    public async Task<IActionResult> Authenticate([FromBody] LoginData loginData)
    {
        try
        {
            string token = await _service.Authenticate(loginData);
            return Ok(new { token });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost()]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        try
        {
            var output = await _service.CreateUser(user);
            return CreatedAtAction("GetUser", new { id = output.UserId }, output);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        try
        {
            await _service.DeleteUser(id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(string id)
    {
        var user = await _service.GetUser(new Guid(id));
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpPut()]
    public async Task<IActionResult> UpdateUser(UserDTO user)
    {
        var updatedUser = await _service.UpdateUser(user);
        return Ok(updatedUser);
    }


}
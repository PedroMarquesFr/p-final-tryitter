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

    [HttpPost()]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        try
        {
            var output = await _service.CreateUser(user);
            return Ok(output);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // [HttpDelete("{id}")]
    // public void DeleteUser(Guid id)
    // {
    //     var user = _repository.Get(id);

    //     if (user == null) return;

    //     _repository.Delete(user);
    // }

    // [HttpGet("{id}")]
    // public User? GetUser(Guid id)
    // {

    //     return _repository.Get(id);
    // }

    // [HttpPut()]
    // public void UpdatePost(User user)
    // {
    //     _repository.Update(user);
    // }


}
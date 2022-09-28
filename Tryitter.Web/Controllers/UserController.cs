using Microsoft.AspNetCore.Mvc;
using Tryitter.Web.Models;
using Tryitter.Web.Repository;

namespace Tryitter.Web.Controllers;

[ApiController]
[Route("user")]
public class UserController : Controller
{
  private readonly IUserRepository _repository;
  public UserController(IUserRepository repository)
  {
    _repository = repository;
  }

  [HttpPost()]
  public IActionResult CreateUser(User user)
  {
    _repository.Add(user);
    return Ok();
  }

  [HttpDelete("{id}")]
  public void DeleteUser(Guid id)
  {
    var user = _repository.Get(id);

    if (user == null) return;

    _repository.Delete(user);
  }

  [HttpGet("{id}")]
  public User? GetUser(Guid id)
  {
    
    return _repository.Get(id);
  }

  [HttpPut()]
  public void UpdatePost(User user)
  {
    _repository.Update(user);
  }


}
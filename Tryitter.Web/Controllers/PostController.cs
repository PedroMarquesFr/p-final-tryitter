using Microsoft.AspNetCore.Mvc;
using Tryitter.Web.Models;
using Tryitter.Web.Repository;
using Tryitter.Web.Services;

namespace Tryitter.Web.Controllers;

[ApiController]
[Route("post")]
public class PostController : Controller
{
    private readonly IPostService _service;
    public PostController(IPostService service)
    {
        _service = service;
    }

    [HttpPost()]
    public async Task<IActionResult> CreatePost([FromBody] Post post)
    {
        try
        {
            var output = await _service.CreatePost(post);
            return CreatedAtAction("GetPost", new { id = output.UserId }, output);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(Guid id)
    {
        try
        {
            await _service.DeletePost(id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
          return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPost(Guid id)
    {
      try
      {
        var post = await _service.GetPost(id)!;
        return Ok(post);
      }
      catch (ArgumentException ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPut()]
    public async Task<IActionResult> UpdatePost(PostDTO post)
    {
        return Ok(await _service.Update(post));
    }


}
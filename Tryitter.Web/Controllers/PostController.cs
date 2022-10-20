using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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
    [Authorize]
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
    [Authorize]
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

    [HttpGet("user/{userId}")]
    [Authorize]
    public async Task<IActionResult> GetPostByUser(Guid userId,[FromQuery] int page, [FromQuery] int take)
    {
        try
        {

            var post = await _service.GetPostsByUser(userId, page, take)!;
            return Ok(post);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut()]
    [Authorize]
    public async Task<IActionResult> UpdatePost(PostDTO post)
    {
        var result = await _service.Update(post);
        return Ok(result);
    }


}
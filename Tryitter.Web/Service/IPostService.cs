using Tryitter.Web.Models;
using Tryitter.Web.Controllers;

namespace Tryitter.Web.Services;

public interface IPostService
{
    public Task<Post> CreatePost(Post post);
    public Task DeletePost(Guid id);
    public Task<Post> Update(PostDTO post);
    public Task<Post>? GetPost(Guid id);
    public Task<ICollection<Post>> GetPostsByUser(Guid UserId, int page, int take);
}
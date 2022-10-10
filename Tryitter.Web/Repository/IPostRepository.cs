using Tryitter.Web.Models;
namespace Tryitter.Web.Repository;

public interface IPostRepository
{
  public Task<Post> Add(Post post);
  public Task Delete(Guid id);
  public Task Update(Post post);
  public Task<Post>? Get(Guid PostId);
  public Task<User> GetPostsByUser(Guid UserId);
}
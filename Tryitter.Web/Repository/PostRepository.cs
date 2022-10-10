using Microsoft.EntityFrameworkCore;
using Tryitter.Web.Models;

namespace Tryitter.Web.Repository;
public class PostRepository : IPostRepository
{
    protected readonly DatabaseContext _context;
    public PostRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<Post> Add(Post post)
    {
        _context.Add(post);

        await _context.SaveChangesAsync();

        return post;
    }
    public async Task Delete(Guid id)
    {

        var result = _context.Post.Single(p => p.PostId == id);

        _context.Remove(result);

        await _context.SaveChangesAsync();

    }

    public async Task Update(Post post)
    {
        _context.Update(post);

        await _context.SaveChangesAsync();
    }

    public async Task<Post>? Get(Guid PostId)
    {
        var post = await _context.Post.FindAsync(PostId);

        return post!;
    }

    public async Task<User> GetPostsByUser(Guid UserId)
    {
        var userWithPosts = await _context.User.AsNoTracking().Where(u => u.UserId == UserId).Include(user => user.Posts).FirstAsync();

        return userWithPosts;
    }

}
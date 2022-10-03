using Microsoft.EntityFrameworkCore;
using Tryitter.Web.Models;

namespace Tryitter.Web.Repository;
public class UserRepository : IUserRepository
{
    protected readonly DatabaseContext _context;
    public UserRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<User> Add(User user)
    {
        _context.Add(user);

        await _context.SaveChangesAsync();
        return user;
    }
    public async Task Delete(Guid id)
    {

        var result = _context.User.Include(e => e.Posts).Single(p => p.UserId == id);
        _context.Remove(result);

        // if (result.Posts is not null) _context.Remove(result.Posts);

        await _context.SaveChangesAsync();
    }

    public async void Update(User user)
    {
        _context.Update(user);

        await _context.SaveChangesAsync();
    }

    public async Task<User?> Get(Guid UserId)
    {
        var user = await _context.User.FindAsync(UserId);
        return user;
    }
    public async Task<User?> GetUserByLoginName(string Login)
    {
        var user = await _context.User.FirstOrDefaultAsync(i => i.Login == Login);
        return user;
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        var users = await _context.User.ToListAsync();

        return users;
    }

}
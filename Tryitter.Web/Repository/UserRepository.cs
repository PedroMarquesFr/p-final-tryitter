using Microsoft.EntityFrameworkCore;
using Tryitter.Web.Models;

namespace Tryitter.Web.Repository;
public class UserRepository: IUserRepository
{
  protected readonly DatabaseContext _context;
  public UserRepository(DatabaseContext context)
  {
    _context = context;
  }

  public virtual void Add(User user)
  {
    _context.Add(user);

    _context.SaveChanges();
  }
  public virtual void Delete(User user)
  {
    
    var result = _context.User.Include(e => e.Posts).Single(p => p.UserId == user.UserId);

    _context.Remove(result);

    _context.Remove(result.Posts);

    _context.SaveChanges();
    
  }

  public virtual void Update(User user)
  {
    _context.Update(user);

    _context.SaveChanges();
  }

  public virtual User? Get(Guid id)
  {
    var user = _context.User.Where(e => e.UserId == id).First();

    return user;
  }

  public virtual IEnumerable<User> GetAll()
  {
    var users = _context.User.ToList();

    return users;
  }

}
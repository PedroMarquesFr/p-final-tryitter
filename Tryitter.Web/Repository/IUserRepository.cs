using Tryitter.Web.Models;
namespace Tryitter.Web.Repository;

public interface IUserRepository
{
  public void Add(User user);
  public void Delete(User user);
  public void Update(User user);
  public User? Get(Guid id);
  public IEnumerable<User> GetAll();
}
using Tryitter.Web.Models;
namespace Tryitter.Web.Services;

public interface IUserService
{
  public Task<User> CreateUser(User user);
//   public void Delete(User user);
//   public void Update(User user);
//   public User? Get(Guid id);
//   public IEnumerable<User> GetAll();
}
using Tryitter.Web.Models;
namespace Tryitter.Web.Repository;

public interface IUserRepository
{
  public Task<User> Add(User user);
  public void Delete(User user);
  public void Update(User user);
  public Task<User?> Get(Guid id);
  public Task<User?> GetUserByLoginName(string Login);
  public Task<IEnumerable<User>> GetAll();
}
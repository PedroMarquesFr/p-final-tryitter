using Tryitter.Web.Models;
namespace Tryitter.Web.Services;

public interface IUserService
{
    public Task<User> CreateUser(User user);
    public Task DeleteUser(Guid id);
    //   public void Update(User user);
    public Task<User?> GetUser(Guid UserId);
    //   public IEnumerable<User> GetAll();
}
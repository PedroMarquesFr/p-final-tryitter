using Tryitter.Web.Models;
namespace Tryitter.Web.Services;

public interface IUserService
{
    public Task<User> CreateUser(User user);
    public Task DeleteUser(Guid id);
    public Task<User> UpdateUser(User user);
    public Task<User?> GetUser(Guid UserId);
    //   public IEnumerable<User> GetAll();
}
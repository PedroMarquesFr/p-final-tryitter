using Tryitter.Web.Models;
using Tryitter.Web.Controllers;
namespace Tryitter.Web.Services;

public interface IUserService
{
    public Task<User> CreateUser(User user);
    public Task DeleteUser(Guid id);
    public Task<User> UpdateUser(UserDTO user);
    public Task<User?> GetUser(Guid UserId);
    public Task<string> Authenticate(LoginData loginData);
    //   public IEnumerable<User> GetAll();
}
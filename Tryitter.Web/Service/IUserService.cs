using Tryitter.Web.Models;
using Tryitter.Web.Controllers;
namespace Tryitter.Web.Services;

public interface IUserService
{
    public Task<User> CreateUser(User user);
    public Task DeleteUser(Guid id);
    public Task<User> UpdateUser(UserDTO user);
    public Task<User?> GetUser(Guid UserId);
    public Task<dynamic> Authenticate(LoginData loginData);
    public Task<User?> CreateUserProfileImage(Guid UserId, byte[] imageIn);
    //   public IEnumerable<User> GetAll();
}
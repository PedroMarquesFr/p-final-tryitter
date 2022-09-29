using Tryitter.Web.Models;
using Tryitter.Web.Repository;

namespace Tryitter.Web.Services;


public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<User> CreateUser(User user)
    {
        var userExists = await _repository.GetUserByLoginName(user.Login)!;
        if (userExists != null) throw new ArgumentException("User already exists");
        var output = await _repository.Add(user);
        return output;
    }

    // public async Task<User> Deleteuser(User user)
    // {
    //     var output = await _repository.Add(user);
    //     return output;
    // }
}
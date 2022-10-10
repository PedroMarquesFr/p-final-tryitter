using Tryitter.Web.Models;
using Tryitter.Web.Repository;
using Tryitter.Web.Controllers;

namespace Tryitter.Web.Services;


public class PostService : IPostService
{
    private readonly IPostRepository _repository;
    private readonly IUserRepository _userRepository;
    public PostService(IPostRepository repository, IUserRepository userRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
    }

    public async Task<Post> CreatePost(Post post)
    {
        var userExist = await _userRepository.Get(post.UserId)!;

        if (userExist is null) throw new InvalidOperationException("User doesn't exists");

        return await _repository.Add(post);
    }

    public async Task DeletePost(Guid id)
    {
        var postExist = await _repository.Get(id)!;

        if (postExist is null) throw new InvalidOperationException("Post doesn't exists");

        await _repository.Delete(postExist.PostId);
    }
    public async Task<Post> Update(PostDTO postdto)
    {
        var postExist = await _repository.Get(postdto.PostId)!;

        if (postExist is null) throw new InvalidOperationException("Post doesn't exists");

        postExist.Content = postdto.Content is null ? postExist.Content : postdto.Content;
        postExist.UpdatedAt = postdto.UpdatedAt;
        postExist.UserId = postdto.UserId;

        await _repository.Update(postExist);

        return postExist;
    }
    public async Task<Post>? GetPost(Guid id)
    {
        var postExist = await _repository.Get(id)!;

        if (postExist is null) throw new InvalidOperationException("Post doesn't exists");

        return postExist;
    }

    public async Task<User> GetPostsByUser(Guid UserId)
    {
        var user = await _repository.GetPostsByUser(UserId);
        return user;
    }
}
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

    public async Task<ICollection<Post>> GetPosts(int page, int take)
    {
        var posts = await _repository.GetPosts(page, take);
        
        return posts;
    }
    public async Task<ICollection<Post>> GetPostsByUser(Guid UserId, int page, int take)
    {
        var userExist = await _userRepository.Get(UserId)!;

        if (userExist is null) throw new InvalidOperationException("User doesn't exists");

        var posts = await _repository.GetPostsByUser(UserId, page, take);

        return posts;
    }
}
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
        // _userRepository
        return await _repository.Add(post);
    }

    public async Task DeletePost(Guid id)
    {
        var postExist = await _repository.Get(id)!;

        if (postExist is null) throw new ArgumentException("Post doesn't exists");

        await _repository.Delete(postExist.PostId);
    }
    public async Task<Post> Update(PostDTO postdto)
    {
        var postExist = await _repository.Get(postdto.PostId)!;

        if (postExist is null) throw new ArgumentException("Post doesn't exists");

        Post newPost = new()
        {
          PostId = postExist.PostId,
          Content = postdto.Content is null ? postExist.Content : postdto.Content,
          CreatedAt = postExist.CreatedAt,
          UpdatedAt = postdto.UpdatedAt,
          UserId = postdto.UserId
        };

        await _repository.Update(newPost);

        return newPost;


    }
    public async Task<Post>? GetPost(Guid id)
    {
        var postExist = await _repository.Get(id)!;

        if (postExist is null) throw new ArgumentException("Post doesn't exists");

        return postExist;
    }

    public async Task<IEnumerable<Post>> GetPostsByUser(Guid UserId)
    {
      return await _repository.GetPostsByUser(UserId);
    }
}
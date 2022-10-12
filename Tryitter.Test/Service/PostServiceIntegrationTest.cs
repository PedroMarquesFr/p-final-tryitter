using Tryitter.Web.Models;
using Tryitter.Test.Repository;
using Tryitter.Web.Services;
using Tryitter.Web.Repository;
using System;
using System.Threading.Tasks;
using System.IO;
using Tryitter.Web.Controllers;

namespace Tryitter.Test.Service
{
    public class PostServiceIntegrationTest
    {
        [Theory]
        [InlineData("Olá, mundo!", "2909656f-3e43-4e88-3c3b-08daa806d4b3")]
        [InlineData("SegundoTeste", "2909656f-3e43-4e88-3c3b-08daa806d4b3")]
        [InlineData("TRYITTER", "2909656f-3e43-4e88-3c3b-08daa806d4b3")]
        public async void ShouldCreateAPost(string content, string postId)
        {
            //Arrange
            User user = new() { Nickname = "teste", Login = "teste@gmail.com", Password = "senha123" };


            var context = new TryitterTestContext();
            var postRepository = new PostRepository(context);
            var userRepository = new UserRepository(context);

            var service = new PostService(postRepository, userRepository);
            var userService = new UserService(userRepository);

            //Act
            var newUser = await userService.CreateUser(user);
            Post post = new() { Content = content, UserId = newUser.UserId, PostId = Guid.Parse(postId)};
            var result = await service.CreatePost(post);

            //Assert
            result.PostId.Should().NotBe(default(Guid));
            result.Content.Should().Be(content);
            // result.UserId.Should().Be(UserId);
        }

        [Theory]
        [InlineData("Olá, mundo!", "2909656f-3e43-4e88-3c3b-08daa806d4b3")]
        [InlineData("SegundoTeste", "2909656f-3e43-4e88-3c3b-08daa806d4b3")]
        [InlineData("TRYITTER", "2909656f-3e43-4e88-3c3b-08daa806d4b3")]
        public async void ShouldDeleteAPost(string content, string postId)
        {
            //Arrange
            User user = new() { Nickname = "teste", Login = "teste@gmail.com", Password = "senha123" };

            var context = new TryitterTestContext();
            var postRepository = new PostRepository(context);
            var userRepository = new UserRepository(context);

            var service = new PostService(postRepository, userRepository);
            var userService = new UserService(userRepository);

            //Act
            var newUser = await userService.CreateUser(user);

            Post post = new() { Content = content, UserId = newUser.UserId, PostId = Guid.Parse(postId)};

            var result = await service.CreatePost(post);

            Func<Task> act = async () => await service.DeletePost(post.PostId);

            //Assert
            await act.Should().NotThrowAsync<InvalidOperationException>();
        }

        [Theory]
        [InlineData("Olá, mundo!", "2909656f-3e43-4e88-3c3b-08daa806d4b3")]
        [InlineData("SegundoTeste", "2909656f-3e43-4e88-3c3b-08daa806d4b3")]
        [InlineData("TRYITTER", "2909656f-3e43-4e88-3c3b-08daa806d4b3")]
        public async void ShouldGetAPost(string content, string postId)
        {
            //Arrange
            User user = new() { Nickname = "teste", Login = "teste@gmail.com", Password = "senha123" };

            var context = new TryitterTestContext();
            var postRepository = new PostRepository(context);
            var userRepository = new UserRepository(context);

            var service = new PostService(postRepository, userRepository);
            var userService = new UserService(userRepository);

            //Act
            var newUser = await userService.CreateUser(user);

            Post post = new() { Content = content, UserId = newUser.UserId, PostId = Guid.Parse(postId)};

            await service.CreatePost(post);

            var result = await service.GetPost(post.PostId)!;

            //Assert
            result.Should().BeOfType<Post>();
            result.Should().BeEquivalentTo(post);
        }

        [Theory]
        [InlineData("Olá, mundo!", "2909656f-3e43-4e88-3c3b-08daa806d4b3", "OLÁ, MUNDO 2.0")]
        [InlineData("SegundoTeste", "2909656f-3e43-4e88-3c3b-08daa806d4b3", "SEGUNDO TESTE DO SEGUNDO TESTE")]
        [InlineData("TRYITTER", "2909656f-3e43-4e88-3c3b-08daa806d4b3", "TRYITTER 2.0")]
        public async void ShouldUpdateAPoster(string content, string postId, string newContent)
        {
            //Arrange
            User user = new() { Nickname = "teste", Login = "teste@gmail.com", Password = "senha123" };

            var context = new TryitterTestContext();
            var postRepository = new PostRepository(context);
            var userRepository = new UserRepository(context);

            var service = new PostService(postRepository, userRepository);
            var userService = new UserService(userRepository);

            //Act
            var newUser = await userService.CreateUser(user);

            Post post = new() { Content = content, UserId = newUser.UserId, PostId = Guid.Parse(postId)};

            var newPost = await service.CreatePost(post);

            PostDTO postDTO = new() { Content = newContent, UserId = newUser.UserId, PostId = newPost.PostId };

            var output = await service.Update(postDTO);

            //Assert
            output.Should().BeOfType<Post>();
            output.Content.Should().Be(newContent);
            output.UserId.Should().Be(newPost.UserId);
            output.PostId.Should().Be(newPost.PostId);
        }

        // [Theory]
        // [InlineData("Olá, mundo!", "2909656f-3e43-4e88-3c3b-08daa806d4b3")]
        // [InlineData("SegundoTeste", "2909656f-3e43-4e88-3c3b-08daa806d4b3")]
        // [InlineData("TRYITTER", "2909656f-3e43-4e88-3c3b-08daa806d4b3")]
        // public async void ShouldGetPostsByUser(string content, string postId)
        // {
        //     //Arrange
        //     User user = new() { Nickname = "teste", Login = "teste@gmail.com", Password = "senha123" };

        //     var context = new TryitterTestContext();
        //     var postRepository = new PostRepository(context);
        //     var userRepository = new UserRepository(context);

        //     var service = new PostService(postRepository, userRepository);
        //     var userService = new UserService(userRepository);

        //     //Act
        //     var newUser = await userService.CreateUser(user);

        //     Post post = new() { Content = content, UserId = newUser.UserId, PostId = Guid.Parse(postId)};

        //     await service.CreatePost(post);

        //     var output = await service.GetPostsByUser(newUser.UserId);

        //     //Assert
        //     output.Posts.Should().NotBeEmpty();

        // }
    }
}
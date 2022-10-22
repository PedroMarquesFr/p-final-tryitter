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
    public class PostRepositoryTest
    {
        [Theory]
        [InlineData("Olá, mundo!", "2909656f-3e43-4e88-3c3b-08daa806d4b3")]
        [InlineData("SegundoTeste", "2909656f-3e43-4e88-3c3b-08daa806d4b3")]
        [InlineData("TRYITTER", "2909656f-3e43-4e88-3c3b-08daa806d4b3")]
        public async void ShouldCreateAPost(string content, string postId)
        {
            //Arrange
            TryitterTestContext tryitterTestContext = new();
            UserRepository userRepository = new(tryitterTestContext);
            PostRepository postRepository = new(tryitterTestContext);

            User user = new() { Nickname = "teste", Login = "teste@gmail.com", Password = "senha123" };

            //Act
            var newUser = await userRepository.Add(user);
            Post post = new() { Content = content, UserId = newUser.UserId, PostId = Guid.Parse(postId)};
            var result = await postRepository.Add(post);

            //Assert
            result.PostId.Should().NotBe(default(Guid));
            result.Content.Should().Be(content);
        }

        [Theory]
        [InlineData("Olá, mundo!", "2909656f-3e43-4e88-3c3b-08daa806d4b3")]
        [InlineData("SegundoTeste", "2909656f-3e43-4e88-3c3b-08daa806d4b3")]
        [InlineData("TRYITTER", "2909656f-3e43-4e88-3c3b-08daa806d4b3")]
        public async void ShouldDeleteAPost(string content, string postId)
        {
            //Arrange
            TryitterTestContext tryitterTestContext = new();
            UserRepository userRepository = new(tryitterTestContext);
            PostRepository postRepository = new(tryitterTestContext);

            User user = new() { Nickname = "teste", Login = "teste@gmail.com", Password = "senha123" };

            //Act
            var newUser = await userRepository.Add(user);
            Post post = new() { Content = content, UserId = newUser.UserId, PostId = Guid.Parse(postId)};

            var newPost = await postRepository.Add(post);

            var savedPost = await postRepository.Get(newPost.PostId)!;
            savedPost.Should().BeEquivalentTo(post);

            await postRepository.Delete(savedPost.PostId);


            //Assert
            var postOnDatabase = await postRepository.Get(savedPost.PostId)!;
            postOnDatabase.Should().Be(null);
        }

        [Theory]
        [InlineData("Olá, mundo!", "2909656f-3e43-4e88-3c3b-08daa806d4b3")]
        [InlineData("SegundoTeste", "2909656f-3e43-4e88-3c3b-08daa806d4b3")]
        [InlineData("TRYITTER", "2909656f-3e43-4e88-3c3b-08daa806d4b3")]
        public async void ShouldGetAPost(string content, string postId)
        {
            //Arrange
            TryitterTestContext tryitterTestContext = new();
            UserRepository userRepository = new(tryitterTestContext);
            PostRepository postRepository = new(tryitterTestContext);

            User user = new() { Nickname = "teste", Login = "teste@gmail.com", Password = "senha123" };

            //Act
            var newUser = await userRepository.Add(user);

            Post post = new() { Content = content, UserId = newUser.UserId, PostId = Guid.Parse(postId)};

            var newPost = await postRepository.Add(post);

            var result = await postRepository.Get(newPost.PostId)!;

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
            TryitterTestContext tryitterTestContext = new();
            UserRepository userRepository = new(tryitterTestContext);
            PostRepository postRepository = new(tryitterTestContext);

            User user = new() { Nickname = "teste", Login = "teste@gmail.com", Password = "senha123" };

            //Act
            var newUser = await userRepository.Add(user);

            Post post = new() { Content = content, UserId = newUser.UserId, PostId = Guid.Parse(postId)};

            var newPost = await postRepository.Add(post);

            newPost.Content = newContent;

            await postRepository.Update(newPost)!;

            //Assert
            newPost.Should().BeOfType<Post>();
            newPost.Content.Should().Be(newContent);
        }

        [Theory]
        [InlineData("Olá, mundo!", "2909656f-3e43-4e88-3c3b-08daa806d4b3")]
        [InlineData("SegundoTeste", "2909656f-3e43-4e88-3c3b-08daa806d4b3")]
        [InlineData("TRYITTER", "2909656f-3e43-4e88-3c3b-08daa806d4b3")]
        public async void ShouldGetPosts(string content, string postId)
        {
            //Arrange
            TryitterTestContext tryitterTestContext = new();
            UserRepository userRepository = new(tryitterTestContext);
            PostRepository postRepository = new(tryitterTestContext);

            User user = new() { Nickname = "teste", Login = "teste@gmail.com", Password = "senha123" };

            //Act
            var newUser = await userRepository.Add(user);

            Post post = new() { Content = content, UserId = newUser.UserId, PostId = Guid.Parse(postId)};

            await postRepository.Add(post);

            var result = await postRepository.GetPosts(0, 3);

            //Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().Contain(post);
        }

        [Theory]
        [InlineData("Olá, mundo!", "2909656f-3e43-4e88-3c3b-08daa806d4b3")]
        [InlineData("SegundoTeste", "2909656f-3e43-4e88-3c3b-08daa806d4b3")]
        [InlineData("TRYITTER", "2909656f-3e43-4e88-3c3b-08daa806d4b3")]
        public async void ShouldGetPostsByUser(string content, string postId)
        {
            //Arrange
            TryitterTestContext tryitterTestContext = new();
            UserRepository userRepository = new(tryitterTestContext);
            PostRepository postRepository = new(tryitterTestContext);

            User user = new() { Nickname = "teste", Login = "teste@gmail.com", Password = "senha123" };

            //Act
            var newUser = await userRepository.Add(user);

            Post post = new() { Content = content, UserId = newUser.UserId, PostId = Guid.Parse(postId)};

            await postRepository.Add(post);

            var result = await postRepository.GetPostsByUser(newUser.UserId, 0, 3);

            //Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().Contain(post);
        }
    }
}
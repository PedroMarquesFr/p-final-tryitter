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
    public class PostServiceTest
    {
        [Theory]
        [InlineData("Olá, mundo!", "2909656f-3e43-4e88-3c3b-08daa806d4b3")]
        [InlineData("SegundoTeste", "2909656f-3e43-4e88-3c3b-08daa806d4b3")]
        [InlineData("TRYITTER", "2909656f-3e43-4e88-3c3b-08daa806d4b3")]
        public async void ShouldCreateAPost(string content, Guid UserId)
        {
            //Arrange
            Post post = new() { Content = content, UserId = UserId };
            var context = new TryitterTestContext();
            var mockRepository = new Mock<IPostRepository>();
            var mockUserRepository = new Mock<IUserRepository>();

            User user = new() { Nickname = "Oi", Login = "email@gmail.com", Password = "12345678" };

            mockUserRepository.Setup(library => library.Get(UserId)).ReturnsAsync(user);
            mockRepository.Setup(library => library.Add(post)).ReturnsAsync(post);

            var service = new PostService(mockRepository.Object, mockUserRepository.Object);

            //Act
            var result = await service.CreatePost(post);

            //Assert
            result.Content.Should().BeEquivalentTo(content);
            result.UserId.Should().Be(UserId);
        }

        [Theory]
        [InlineData("Olá, mundo!", "2909656f-3e43-4e88-3c3b-08daa806d4b3")]
        [InlineData("SegundoTeste", "2909656f-3e43-4e88-3c3b-08daa806d4b3")]
        [InlineData("TRYITTER", "2909656f-3e43-4e88-3c3b-08daa806d4b3")]
        public async void ShouldThrownAnErrorWhenUserDoesntExists(string content, Guid UserId)
        {
            //Arrange
            Post post = new() { Content = content, UserId = UserId };
            var context = new TryitterTestContext();
            var mockRepository = new Mock<IPostRepository>();
            var mockUserRepository = new Mock<IUserRepository>();

            mockUserRepository.Setup(library => library.Get(UserId)).ReturnsAsync(value: null);
            mockRepository.Setup(library => library.Add(post)).ReturnsAsync(post);

            var service = new PostService(mockRepository.Object, mockUserRepository.Object);

            //Act
            Func<Task> act = async () => await service.CreatePost(post);

            //Assert
            await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("User doesn't exists");
        }

        [Theory]
        [InlineData("Olá, mundo!", "2909656f-3e43-4e88-3c3b-08daa806d4b3")]
        [InlineData("SegundoTeste", "2909656f-3e43-4e88-3c3b-08daa806d4b3")]
        [InlineData("TRYITTER", "2909656f-3e43-4e88-3c3b-08daa806d4b3")]
        public async void ShouldDeleteAPost(string content, Guid UserId)
        {
            //Arrange
            Post post = new() { Content = content, UserId = UserId };
            User user = new() { Nickname = "Oi", Login = "email@gmail.com", Password = "12345678" };

            var context = new TryitterTestContext();
            var mockRepository = new Mock<IPostRepository>();
            var mockUserRepository = new Mock<IUserRepository>();

            mockRepository.Setup(library => library.Get(post.PostId)).ReturnsAsync(post);
            mockRepository.Setup(library => library.Add(post)).ReturnsAsync(post);

            var service = new PostService(mockRepository.Object, mockUserRepository.Object);

            //Act
            Func<Task> act = async () => await service.DeletePost(post.PostId);

            //Assert

            await act.Should().NotThrowAsync<InvalidOperationException>();
        }

        [Theory]
        [InlineData("Post Errado!", "b420448b-5c1d-4e4a-13e8-08daa72e6a7e")]
        [InlineData("Erro 2!", "b420448b-5c1d-4e4a-13e8-08daa72e6a7e")]
        [InlineData("Errado!", "b420448b-5c1d-4e4a-13e8-08daa72e6a7e")]
        public async void ShouldThrownAnErrorWhenPostDoesntExist(string content, Guid UserId)
        {
            Post post = new() { Content = content, UserId = UserId };
            User user = new() { Nickname = "Oi", Login = "email@gmail.com", Password = "12345678" };

            var context = new TryitterTestContext();
            var mockRepository = new Mock<IPostRepository>();
            var mockUserRepository = new Mock<IUserRepository>();

            mockRepository.Setup(library => library.Get(post.PostId))!.ReturnsAsync(value: null);
            mockRepository.Setup(library => library.Add(post))!.ReturnsAsync(value: null);

            var service = new PostService(mockRepository.Object, mockUserRepository.Object);

            //Act
            Func<Task> act = async () => await service.DeletePost(post.PostId);

            //Assert

            await act.Should().ThrowAsync<InvalidOperationException>();
        }

        [Theory]
        [InlineData("Olá, mundo!", "2909656f-3e43-4e88-3c3b-08daa806d4b3")]
        [InlineData("SegundoTeste", "2909656f-3e43-4e88-3c3b-08daa806d4b3")]
        [InlineData("TRYITTER", "2909656f-3e43-4e88-3c3b-08daa806d4b3")]
        public async void ShouldGetAPost(string content, Guid UserId)
        {
            //Arrange
            Post post = new() { Content = content, UserId = UserId };
            User user = new() { Nickname = "Oi", Login = "email@gmail.com", Password = "12345678" };

            var context = new TryitterTestContext();
            var mockRepository = new Mock<IPostRepository>();
            var mockUserRepository = new Mock<IUserRepository>();


            mockRepository.Setup(library => library.Get(post.PostId)).ReturnsAsync(post);
            mockRepository.Setup(library => library.Add(post)).ReturnsAsync(post);

            var service = new PostService(mockRepository.Object, mockUserRepository.Object);

            //Act
            Func<Task> act = async () => await service.GetPost(post.PostId)!;

            //Assert
            await act.Should().NotThrowAsync<InvalidDataException>();
        }

        [Theory]
        [InlineData("Post Errado!", "b420448b-5c1d-4e4a-13e8-08daa72e6a7e")]
        [InlineData("Erro 2!", "b420448b-5c1d-4e4a-13e8-08daa72e6a7e")]
        [InlineData("Errado!", "b420448b-5c1d-4e4a-13e8-08daa72e6a7e")]
        public async void ShouldThrownAnErrorWhenPostIsNotFound(string content, Guid UserId)
        {
            //Arrange
            Post post = new() { Content = content, UserId = UserId };
            User user = new() { Nickname = "Oi", Login = "email@gmail.com", Password = "12345678" };

            var context = new TryitterTestContext();
            var mockRepository = new Mock<IPostRepository>();
            var mockUserRepository = new Mock<IUserRepository>();


            mockRepository.Setup(library => library.Get(post.PostId))!.ReturnsAsync(value: null);
            mockRepository.Setup(library => library.Add(post))!.ReturnsAsync(value: null);

            var service = new PostService(mockRepository.Object, mockUserRepository.Object);

            //Act
            Func<Task> act = async () => await service.GetPost(post.PostId)!;

            //Assert
            await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Post doesn't exists");
        }

        [Theory]
        [InlineData("Olá, mundo!", "2909656f-3e43-4e88-3c3b-08daa806d4b3", "OLÁ, MUNDO 2.0")]
        [InlineData("SegundoTeste", "2909656f-3e43-4e88-3c3b-08daa806d4b3", "SEGUNDO TESTE DO SEGUNDO TESTE")]
        [InlineData("TRYITTER", "2909656f-3e43-4e88-3c3b-08daa806d4b3", "TRYITTER 2.0")]
        public async void ShouldUpdateAnUser(string content, Guid UserId, string newContent)
        {
            //Arrange
            Post post = new() { Content = content, UserId = UserId };
            PostDTO postDTO = new() { Content = newContent, UserId = UserId };
            User user = new() { Nickname = "Oi", Login = "email@gmail.com", Password = "12345678" };

            var context = new TryitterTestContext();
            var mockRepository = new Mock<IPostRepository>();
            var mockUserRepository = new Mock<IUserRepository>();

            mockRepository.Setup(library => library.Get(post.PostId)).ReturnsAsync(post);
            mockRepository.Setup(library => library.Add(post)).ReturnsAsync(post);

            var service = new PostService(mockRepository.Object, mockUserRepository.Object);

            //Act
            var output = await service.Update(postDTO);

            //Assert
            output.Should().BeOfType<Post>();
            output.Content.Should().Be(newContent);
            output.UserId.Should().Be(UserId);
        }

        [Theory]
        [InlineData("Errado!", "b420448b-5c1d-4e4a-13e8-08daa72e6a7e", "Errado! 2.0")]
        [InlineData("Post errado!", "b420448b-5c1d-4e4a-13e8-08daa72e6a7e", "Post Errado! 2.0")]
        [InlineData("Post inválido!", "b420448b-5c1d-4e4a-13e8-08daa72e6a7e", "Post inválido! 2.0")]
        public async void ShouldThrownAnErrorWhenUpdatingPostThatDoesntExist(string content, Guid UserId, string newContent)
        {
            //Arrange
            Post post = new() { Content = content, UserId = UserId };
            PostDTO postDTO = new() { Content = newContent, UserId = UserId };
            User user = new() { Nickname = "Oi", Login = "email@gmail.com", Password = "12345678" };

            var context = new TryitterTestContext();
            var mockRepository = new Mock<IPostRepository>();
            var mockUserRepository = new Mock<IUserRepository>();

            mockRepository.Setup(library => library.Get(post.PostId))!.ReturnsAsync(value: null);
            mockRepository.Setup(library => library.Add(post))!.ReturnsAsync(value: null);

            var service = new PostService(mockRepository.Object, mockUserRepository.Object);

            //Act
            // var output = await service.Update(postDTO);
            Func<Task> act = async () => await service.Update(postDTO)!;

            //Assert
            await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Post doesn't exists");
        }

        // [Theory]
        // [InlineData("Olá, mundo!", "2909656f-3e43-4e88-3c3b-08daa806d4b3")]
        // [InlineData("SegundoTeste", "2909656f-3e43-4e88-3c3b-08daa806d4b3")]
        // [InlineData("TRYITTER", "2909656f-3e43-4e88-3c3b-08daa806d4b3")]
        // public async void ShouldGetPostsByUser(string content, Guid UserId)
        // {
        //     //Arrange
        //     Post post = new() { Content = content, UserId = UserId };
        //     User user = new() { Nickname = "Oi", Login = "email@gmail.com", Password = "12345678" };

        //     var context = new TryitterTestContext();
        //     var mockRepository = new Mock<IPostRepository>();
        //     var mockUserRepository = new Mock<IUserRepository>();


        //     mockUserRepository.Setup(library => library.Get(UserId)).ReturnsAsync(user);
        //     // mockRepository.Setup(library => library.Get(post.PostId)).ReturnsAsync(post);
        //     mockRepository.Setup(library => library.Add(post)).ReturnsAsync(post);

        //     var service = new PostService(mockRepository.Object, mockUserRepository.Object);

        //     //Act
        //     var output = await service.GetPostsByUser(UserId)!;

        //     //Assert
        //     // await act.Should().NotThrowAsync<InvalidDataException>();
        //     output.Should().Contain(post);

        // }

        // [Theory]
        // [InlineData("Olá, mundo!", "2909656f-3e43-4e88-3c3b-08daa806d4b3")]
        // [InlineData("SegundoTeste", "2909656f-3e43-4e88-3c3b-08daa806d4b3")]
        // [InlineData("TRYITTER", "2909656f-3e43-4e88-3c3b-08daa806d4b3")]
        // public async void ShouldThrowExceptionWhenUserIsNotFound(string content, Guid UserId)
        // {
        //     //Arrange
        //     Post post = new() { Content = content, UserId = UserId };
        //     User user = new() { Nickname = "Oi", Login = "email@gmail.com", Password = "12345678" };

        //     var context = new TryitterTestContext();
        //     var mockRepository = new Mock<IPostRepository>();
        //     var mockUserRepository = new Mock<IUserRepository>();


        //     mockUserRepository.Setup(library => library.Get(UserId)).ReturnsAsync(user);
        //     // mockRepository.Setup(library => library.Get(post.PostId)).ReturnsAsync(post);
        //     mockRepository.Setup(library => library.Add(post)).ReturnsAsync(post);

        //     var service = new PostService(mockRepository.Object, mockUserRepository.Object);

        //     //Act
        //     Func<Task> act = async () => await service.GetPostsByUser(UserId)!;

        //     //Assert
        //     await act.Should().NotThrowAsync<InvalidDataException>();
        // }
    }
}
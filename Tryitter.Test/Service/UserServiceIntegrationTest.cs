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
    public class UserServiceIntegrationTest
    {
        [Theory]
        [InlineData("Joao", "joao123@gmail.com", "1234")]
        [InlineData("Albert", "1234@gmail.com", "senha123")]
        [InlineData("a", "a123@gmail.com", "1010")]
        public async void ShouldCreateAnUser(string nickname, string login, string password)
        {
            //Arrange
            User user = new() { Nickname = nickname, Login = login, Password = password };
            var context = new TryitterTestContext();
            var repository = new UserRepository(context);

            var service = new UserService(repository);

            //Act
            var result = await service.CreateUser(user);

            //Assert
            result.UserId.Should().NotBe(default(Guid));
            result.Login.Should().Be(login);
            result.Password.Should().Be(password);
            result.Nickname.Should().Be(nickname);
        }

        [Theory]
        [InlineData("Joao", "joao123@gmail.com", "1234")]
        [InlineData("Albert", "1234@gmail.com", "senha123")]
        [InlineData("a", "a123@gmail.com", "1010")]
        public async void ShouldDeleteAnUser(string nickname, string login, string password)
        {
            //Arrange
            User user = new() { Nickname = nickname, Login = login, Password = password };
            var context = new TryitterTestContext();
            var repository = new UserRepository(context);

            var service = new UserService(repository);

            //Act
            var newUser = await service.CreateUser(user);
            Func<Task> act = async () => await service.DeleteUser(newUser.UserId);

            //Assert
            await act.Should().NotThrowAsync<ArgumentException>();
        }

        [Theory]
        [InlineData("Joao", "joao123@gmail.com", "1234")]
        [InlineData("Albert", "1234@gmail.com", "senha123")]
        [InlineData("a", "a123@gmail.com", "1010")]
        public async void ShouldGetAnUser(string nickname, string login, string password)
        {
            //Arrange
            User user = new() { Nickname = nickname, Login = login, Password = password };
            var context = new TryitterTestContext();
            var repository = new UserRepository(context);
            var service = new UserService(repository);

            //Act
            var newUser = await service.CreateUser(user);
            var output = await service.GetUser(newUser.UserId);

            //Assert
            output.Should().BeOfType<User>();
            output.Should().BeEquivalentTo(user);
        }

        [Theory]
        [InlineData("Joao", "joao123@gmail.com", "1234", "NewJoao", "joao123@gmail.com", "1234")]
        [InlineData("Albert", "1234@gmail.com", "senha123", "Albert", "new1234@gmail.com", "1234")]
        [InlineData("a", "a123@gmail.com", "1010", "Albert", "new1234@gmail.com", "new1010")]
        public async void ShouldUpdateAnUser(string nickname, string login, string password, string newNickname, string newLogin, string newPassword)
        {
            //Arrange
            User user = new() { Nickname = nickname, Login = login, Password = password };
            UserDTO userDTO = new() { Nickname = newNickname, Login = newLogin, Password = newPassword };
            var context = new TryitterTestContext();
            var repository = new UserRepository(context);
            var service = new UserService(repository);

            //Act
            var newUser = await service.CreateUser(user);
            userDTO.UserId = newUser.UserId;
            var output = await service.UpdateUser(userDTO);

            //Assert
            output.Should().BeOfType<User>();
            output.Nickname.Should().Be(newNickname);
            output.Login.Should().Be(login);
            output.Password.Should().Be(newPassword);
        }
    }
}
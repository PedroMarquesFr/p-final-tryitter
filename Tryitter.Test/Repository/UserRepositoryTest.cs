using Tryitter.Web.Models;
using Tryitter.Test.Repository;
using Tryitter.Web.Services;
using Tryitter.Web.Repository;
using System;
using System.Threading.Tasks;

namespace Tryitter.Test.Service
{
    public class UserRepositoryTest
    {
        [Theory]
        [InlineData("Joao", "joao123", "1234")]
        [InlineData("Albert", "1234", "senha123")]
        [InlineData("a", "a123", "1010")]
        public async void ShouldCreateAnUser(string nickname, string login, string password)
        {
            //Arrange
            TryitterTestContext tryitterTestContext = new();
            UserRepository userRepository = new(tryitterTestContext);

            User user = new() { Nickname = nickname, Login = login, Password = password };

            //Act
            var result = await userRepository.Add(user);
            var userSaved = await userRepository.Get(result.UserId);

            //Assert
            result.Should().Be(user);
            userSaved.Should().BeEquivalentTo(user);

        }

        [Theory]
        [InlineData("Joao", "joao123", "1234")]
        [InlineData("Albert", "1234", "senha123")]
        [InlineData("a", "a123", "1010")]
        public async void ShouldDeleteAnUser(string nickname, string login, string password)
        {
            //Arrange
            TryitterTestContext tryitterTestContext = new();
            UserRepository userRepository = new(tryitterTestContext);

            User user = new() { Nickname = nickname, Login = login, Password = password };

            //Act
            var result = await userRepository.Add(user);
            var userSaved = await userRepository.Get(result.UserId);
            userSaved.Should().BeEquivalentTo(user);
            await userRepository.Delete(result.UserId);

            //Assert
            var userOnDatabase = await userRepository.Get(result.UserId);
            userOnDatabase.Should().Be(null);

        }

        [Theory]
        [InlineData("Joao", "joao123", "1234")]
        [InlineData("Albert", "1234", "senha123")]
        [InlineData("a", "a123", "1010")]
        public async void ShouldUpdateAnUser(string nickname, string login, string password)
        {
            //Arrange
            var novoPassword = "novoPassword";
            TryitterTestContext tryitterTestContext = new();
            UserRepository userRepository = new(tryitterTestContext);

            User user = new() { Nickname = nickname, Login = login, Password = password };

            //Act
            var result = await userRepository.Add(user);
            var userSaved = await userRepository.Get(result.UserId);
            userSaved.Should().BeEquivalentTo(user);
            userSaved!.Password = novoPassword;
            await userRepository.Update(userSaved);

            //Assert
            var userOnDatabase = await userRepository.Get(result.UserId);
            userOnDatabase!.Password.Should().Be(novoPassword);

        }

        [Theory]
        [InlineData("Joao", "joao123", "1234")]
        [InlineData("Albert", "1234", "senha123")]
        [InlineData("a", "a123", "1010")]
        public async void ShouldGetAnUser(string nickname, string login, string password)
        {
            //Arrange
            TryitterTestContext tryitterTestContext = new();
            UserRepository userRepository = new(tryitterTestContext);

            User user = new() { Nickname = nickname, Login = login, Password = password };

            //Act
            var result = await userRepository.Add(user);
            var userSaved = await userRepository.Get(result.UserId);

            //Assert
            userSaved.Should().BeEquivalentTo(user);

        }

        [Theory]
        [InlineData("Joao", "joao123", "1234")]
        [InlineData("Albert", "1234", "senha123")]
        [InlineData("a", "a123", "1010")]
        public async void ShouldGetAllUsers(string nickname, string login, string password)
        {
            //Arrange
            TryitterTestContext tryitterTestContext = new();
            UserRepository userRepository = new(tryitterTestContext);

            User user = new() { Nickname = nickname, Login = login, Password = password };
            User user2 = new() { Nickname = nickname, Login = login, Password = password };
            var users = new User[] { user, user2 };

            //Act
            await userRepository.Add(user);
            await userRepository.Add(user2);
            var userSaved = await userRepository.GetAll();

            //Assert
            userSaved.Should().BeEquivalentTo(users);

        }


        [Theory]
        [InlineData("Joao", "joao123", "1234")]
        [InlineData("Albert", "1234", "senha123")]
        [InlineData("a", "a123", "1010")]
        public async void ShouldGetUserByLoginName(string nickname, string login, string password)
        {
            //Arrange
            TryitterTestContext tryitterTestContext = new();
            UserRepository userRepository = new(tryitterTestContext);

            User user = new() { Nickname = nickname, Login = login, Password = password };

            //Act
            var result = await userRepository.Add(user);
            var userSaved = await userRepository.GetUserByLoginName(user.Login);

            //Assert
            result.Should().BeEquivalentTo(userSaved);

        }
    }
}
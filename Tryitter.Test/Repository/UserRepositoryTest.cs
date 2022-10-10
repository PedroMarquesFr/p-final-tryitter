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
            var userOnDatabase = await userRepository.GetAll();
            // userOnDatabase.Count.Should().BeEmpty();

        }
    }
}
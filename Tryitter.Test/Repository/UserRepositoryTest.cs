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

        // [Theory]
        // [InlineData("Joao", "joao123", "1234")]
        // [InlineData("Albert", "1234", "senha123")]
        // [InlineData("a", "a123", "1010")]
        // public async void ShouldThrownAnErrorWhenUserWithTheSameLoginAlreadyExists(string nickname, string login, string password)
        // {
        //     //Arrange
        //     User user = new() { Nickname = nickname, Login = login, Password = password };
        //     var context = new TryitterTestContext();
        //     var mockRepository = new Mock<IUserRepository>();

        //     mockRepository.Setup(library => library.GetUserByLoginName(login)).ReturnsAsync(user); //forcar a funcao a retornar um valor assincrono
        //     mockRepository.Setup(library => library.Add(user)).ReturnsAsync(user);

        //     var service = new UserService(mockRepository.Object);

        //     //Act
        //     Func<Task> act = async () => await service.CreateUser(user);

        //     //Assert
        //     await act.Should().ThrowAsync<ArgumentException>();
        // }
    }
}
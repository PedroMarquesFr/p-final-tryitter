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
    public class UserServiceTest
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
            var mockRepository = new Mock<IUserRepository>();

            mockRepository.Setup(library => library.GetUserByLoginName(login)).ReturnsAsync(value: null); //forcar a funcao a retornar um valor assincrono
            mockRepository.Setup(library => library.Add(user)).ReturnsAsync(user);

            var service = new UserService(mockRepository.Object);

            //Act
            var result = await service.CreateUser(user);

            //Assert
            // result.UserId.Should().NotBe(default(Guid));
            result.Login.Should().Be(login);
            result.Password.Should().Be(password);
            result.Nickname.Should().Be(nickname);
        }

        [Theory]
        [InlineData("Joao", "joao123@gmail.com", "1234")]
        [InlineData("Albert", "1234@gmail.com", "senha123")]
        [InlineData("a", "a123@gmail.com", "1010")]
        public async void ShouldThrownAnErrorWhenUserWithTheSameLoginAlreadyExists(string nickname, string login, string password)
        {
            //Arrange
            User user = new() { Nickname = nickname, Login = login, Password = password };
            var context = new TryitterTestContext();
            var mockRepository = new Mock<IUserRepository>();

            mockRepository.Setup(library => library.GetUserByLoginName(login)).ReturnsAsync(user); //forcar a funcao a retornar um valor assincrono
            mockRepository.Setup(library => library.Add(user)).ReturnsAsync(user);

            var service = new UserService(mockRepository.Object);

            //Act
            Func<Task> act = async () => await service.CreateUser(user);

            //Assert
            await act.Should().ThrowAsync<ArgumentException>();
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
            var mockRepository = new Mock<IUserRepository>();

            mockRepository.Setup(library => library.Get(user.UserId)).ReturnsAsync(user); //forcar a funcao a retornar um valor assincrono
            mockRepository.Setup(library => library.Delete(user.UserId));

            var service = new UserService(mockRepository.Object);

            //Act
            Func<Task> act = async () => await service.DeleteUser(user.UserId);

            //Assert

            await act.Should().NotThrowAsync<ArgumentException>();
        }

        [Theory]
        [InlineData("Joao", "joao123@gmail.com", "1234")]
        [InlineData("Albert", "1234@gmail.com", "senha123")]
        [InlineData("a", "a123@gmail.com", "1010")]
        public async void ShouldThrownAnErrorWhenUserDoesntExist(string nickname, string login, string password)
        {
            //Arrange
            User user = new() { Nickname = nickname, Login = login, Password = password };
            var context = new TryitterTestContext();
            var mockRepository = new Mock<IUserRepository>();

            mockRepository.Setup(library => library.Get(user.UserId)).ReturnsAsync(value: null); //forcar a funcao a retornar um valor assincrono

            var service = new UserService(mockRepository.Object);

            //Act
            Func<Task> act = async () => await service.DeleteUser(user.UserId);

            //Assert

            await act.Should().ThrowAsync<ArgumentException>();
        }

        [Theory]
        [InlineData("Joao", "joao123@gmail.com", "1234")]
        [InlineData("Albert", "1234@gmail.com", "senha123")]
        [InlineData("a", "a123@gmail.com", "1010")]
        public async void ShouldGetAnUser(string nickname, string login, string password)
        {
            //Arrange
            User user = new() { Nickname = nickname, Login = login, Password = password };
            var mockRepository = new Mock<IUserRepository>();

            mockRepository.Setup(library => library.Get(user.UserId)).ReturnsAsync(user); //forcar a funcao a retornar um valor assincrono

            var service = new UserService(mockRepository.Object);

            //Act
            var output = await service.GetUser(user.UserId);

            //Assert
            output.Should().BeOfType<User>();
            output.Should().Be(user);
        }

        [Theory]
        [InlineData("Joao", "joao123@gmail.com", "1234")]
        [InlineData("Albert", "1234@gmail.com", "senha123")]
        [InlineData("a", "a123@gmail.com", "1010")]
        public async void ShouldThrownAnErrorWhenUserIsNotFound(string nickname, string login, string password)
        {
            //Arrange
            User user = new() { Nickname = nickname, Login = login, Password = password };
            // var context = new TryitterTestContext();
            var mockRepository = new Mock<IUserRepository>();

            mockRepository.Setup(library => library.Get(user.UserId)).ReturnsAsync(value: null); //forcar a funcao a retornar um valor assincrono

            var service = new UserService(mockRepository.Object);

            //Act
            Func<Task> act = async () => await service.GetUser(user.UserId);

            //Assert

            await act.Should().ThrowAsync<InvalidDataException>().WithMessage("User not found");
        }

        [Theory]
        [InlineData("Joao", "joao123@gmail.com", "1234", "NewJoao", "joao123@gmail.com", "1234")]
        [InlineData("Albert", "1234@gmail.com", "senha123", "Albert", "new1234@gmail.com", "1234")]
        [InlineData("a", "a123@gmail.com", "1010", "Albert", "new1234@gmail.com", "new1010")]
        public async void ShouldUpdateAnUser(string nickname, string login, string password, string newNickname, string newLogin, string newPassword)
        {
            //Arrange
            User user = new() { Nickname = nickname, Login = login, Password = password };
            // user.UserId = userId;
            UserDTO userDTO = new() { Nickname = newNickname, Login = newLogin, Password = newPassword };
            User newUser = new() { UserId = user.UserId, Nickname = newNickname, Login = login, Password = newPassword };
            var mockRepository = new Mock<IUserRepository>();

            mockRepository.Setup(library => library.Get(user.UserId)).ReturnsAsync(user); //forcar a funcao a retornar um valor assincrono
            mockRepository.Setup(library => library.Update(user));

            var service = new UserService(mockRepository.Object);

            //Act
            var output = await service.UpdateUser(userDTO);

            //Assert
            output.Should().BeOfType<User>();
            output.Nickname.Should().Be(newNickname);
            output.Login.Should().Be(login);
            output.Password.Should().Be(newPassword);
        }

        [Theory]
        [InlineData("Joao", "joao123@gmail.com", "1234", "NewJoao", "joao123@gmail.com", "1234")]
        [InlineData("Albert", "1234@gmail.com", "senha123", "Albert", "new1234@gmail.com", "1234")]
        [InlineData("a", "a123@gmail.com", "1010", "Albert", "new1234@gmail.com", "new1010")]
        public async void ShouldThrownAnErrorWhenUserDoesntExists(string nickname, string login, string password, string newNickname, string newLogin, string newPassword)
        {
            //Arrange
            User user = new() { Nickname = nickname, Login = login, Password = password };
            UserDTO userDTO = new() { Nickname = newNickname, Login = newLogin, Password = newPassword };
            User newUser = new() { UserId = user.UserId, Nickname = newNickname, Login = login, Password = newPassword };
            var mockRepository = new Mock<IUserRepository>();

            mockRepository.Setup(library => library.Get(user.UserId)).ReturnsAsync(value: null); //forcar a funcao a retornar um valor assincrono

            var service = new UserService(mockRepository.Object);

            //Act
            Func<Task> act = async () => await service.UpdateUser(userDTO);     

            //Assert
            await act.Should().ThrowAsync<ArgumentException>().WithMessage("User doesnt exists.");
        }
    }
}
// using Tryitter.Web.Models;
// using Tryitter.Web.Services;
// using Tryitter.Web.Repository;
// using Tryitter.Web.Controllers;
// using System;
// using Microsoft.AspNetCore.Mvc;
// using System.Net.Http;
// using System.Net.Http.Headers;
// using Microsoft.AspNetCore.TestHost;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.AspNetCore.Mvc.Testing;
// using System.Threading.Tasks;
// using Newtonsoft.Json;
// using System.Text;
// using Microsoft.Extensions.Hosting;

// namespace Tryitter.Test.Controller;

// public class UserControllerTest : IClassFixture<TestingWebAppFactory<Program>>
// {

//     private readonly HttpClient _client;

//     public UserControllerTest(TestingWebAppFactory<Program> factory)
//         => _client = factory.CreateClient();

//     [Trait("User", "1 - User")]
//     [Fact(DisplayName = "Teste para Create User")]
//     public async Task TestCreateAnUserController()
//     {
    
//     var newUser = JsonConvert.SerializeObject(new
//     {
//         Nickname = "User 3",
//         Login = "email3@email.com",
//         Password = "12345678"
//     });

//     var response = await _client.PostAsync("user", new StringContent(newUser, Encoding.UTF8, "application/json"));
//     response.Should().BeSuccessful();
//     }
// }

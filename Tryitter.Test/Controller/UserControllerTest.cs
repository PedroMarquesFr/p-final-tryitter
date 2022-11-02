using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using RichardSzalay.MockHttp;
using System;

namespace Tryitter.Test.Controller;

public class UserControllerTest
{
    public string apiUri = "https://localhost:7272";

    
    [Trait("User", "1 - User")]
    [Fact(DisplayName = "Teste para Create User")]
    public async Task TestCreateAnUserController()
    {
    
        var mockHttp = new MockHttpMessageHandler();

        var newUser = JsonConvert.SerializeObject(new
          {
              Nickname = "User 1",
              Login = "email1@email.com",
              Password = "12345678"
          });

        // Setup a respond for the user api (including a wildcard in the URL)
        mockHttp.When(HttpMethod.Post, $"{apiUri}/user")
                .Respond("application/json", newUser); // Respond with JSON

        // Inject the handler or client into your application code
        var client = mockHttp.ToHttpClient();

      
        // var response = await client.GetAsync("http://localhost/api/user/1234");
        var response = await client.PostAsync($"{apiUri}/user", new StringContent(newUser, Encoding.UTF8, "application/json"));
        // or without async: var response = client.GetAsync("http://localhost/api/user/1234").Result;

        var json = await response.Content.ReadAsStringAsync();

        // No network connection required
        response.Should().BeSuccessful();
        json.Should().Contain(newUser);

    }

    [Trait("User", "1 - User")]
    [Fact(DisplayName = "Teste para Delete User")]
    public async Task TestDeleteAnUserController()
    {
    
        var mockHttp = new MockHttpMessageHandler();

        var newUser = JsonConvert.SerializeObject(new
          {
              Nickname = "User 1",
              Login = "email1@email.com",
              Password = "12345678"
          });

        mockHttp.When(HttpMethod.Post, $"{apiUri}/user")
                .Respond("application/json", newUser);

        mockHttp.When(HttpMethod.Get, $"{apiUri}/user/*")
                .Respond("application/json", newUser);

        mockHttp.When(HttpMethod.Delete, $"{apiUri}/user/*")
                .Respond(System.Net.HttpStatusCode.NoContent);
                
        var client = mockHttp.ToHttpClient();

        var response = await client.PostAsync($"{apiUri}/user", new StringContent(newUser, Encoding.UTF8, "application/json"));
        response.Should().BeSuccessful();

        var getUser = await client.GetAsync($"{apiUri}/user/idqualquermockado");
        getUser.Should().BeSuccessful();

        var deleteUser = await client.DeleteAsync($"{apiUri}/user/idqualquermockado");
        deleteUser.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);

    }

    [Trait("User", "1 - User")]
    [Fact(DisplayName = "Teste para Get User")]
    public async Task TestGetAnUserController()
    {
    
        var mockHttp = new MockHttpMessageHandler();

        var newUser = JsonConvert.SerializeObject(new
          {
              Nickname = "User 1",
              Login = "email1@email.com",
              Password = "12345678"
          });

        mockHttp.When(HttpMethod.Post, $"{apiUri}/user")
                .Respond("application/json", newUser);

        mockHttp.When(HttpMethod.Get, $"{apiUri}/user/*")
                .Respond("application/json", newUser);
                
        var client = mockHttp.ToHttpClient();

        var response = await client.PostAsync($"{apiUri}/user", new StringContent(newUser, Encoding.UTF8, "application/json"));
        response.Should().BeSuccessful();

        var getUser = await client.GetAsync($"{apiUri}/user/idqualquermockado");
        getUser.Should().BeSuccessful();

        var json = await response.Content.ReadAsStringAsync();
        json.Should().Contain(newUser);

    }

    [Trait("User", "1 - User")]
    [Fact(DisplayName = "Teste para Update User")]
    public async Task TestUpdateAnUserController()
    {
    
        var mockHttp = new MockHttpMessageHandler();

        var newUser = JsonConvert.SerializeObject(new
          {
              Nickname = "User 1",
              Login = "email1@email.com",
              Password = "12345678"
          });

        var updatedUser = JsonConvert.SerializeObject(new
          {
              Nickname = "Usuário Atualizado",
              Login = "novoemfolha@novouser.com",
              Password = "senhasegura123"
          });

        mockHttp.When(HttpMethod.Post, $"{apiUri}/user")
                .Respond("application/json", newUser);

        mockHttp.When(HttpMethod.Put, $"{apiUri}/user")
                .Respond("application/json", updatedUser);
                
        var client = mockHttp.ToHttpClient();

        var response = await client.PostAsync($"{apiUri}/user", new StringContent(newUser, Encoding.UTF8, "application/json"));
        response.Should().BeSuccessful();

        var getUser = await client.PutAsync($"{apiUri}/user", new StringContent(updatedUser, Encoding.UTF8, "application/json"));
        getUser.Should().BeSuccessful();

        var json = await getUser.Content.ReadAsStringAsync();
        json.Should().Contain(updatedUser);

    }
}

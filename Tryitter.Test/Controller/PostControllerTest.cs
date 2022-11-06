using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using RichardSzalay.MockHttp;
using System;

namespace Tryitter.Test.Controller;

public class PostControllerTest
{
    public string apiUri = "https://localhost:7272";

    
    [Trait("Post", "2 - Post")]
    [Fact(DisplayName = "Teste para Create Post")]
    public async Task TestCreateAPostController()
    {
    
        var mockHttp = new MockHttpMessageHandler();

        var newPost = JsonConvert.SerializeObject(new
          {
              Content = "Um post qualquer",
              UserId = "53912546-34dc-4c60-75b8-08dab2cc3a7a"
          });

        // Setup a respond for the user api (including a wildcard in the URL)
        mockHttp.When(HttpMethod.Post, $"{apiUri}/post")
                .Respond("application/json", newPost); // Respond with JSON

        // Inject the handler or client into your application code
        var client = mockHttp.ToHttpClient();

      
        // var response = await client.GetAsync("http://localhost/api/user/1234");
        var response = await client.PostAsync($"{apiUri}/post", new StringContent(newPost, Encoding.UTF8, "application/json"));
        // or without async: var response = client.GetAsync("http://localhost/api/user/1234").Result;

        var json = await response.Content.ReadAsStringAsync();

        // No network connection required
        response.Should().BeSuccessful();
        json.Should().Contain(newPost);

    }

    [Trait("Post", "2 - Post")]
    [Fact(DisplayName = "Teste para Delete Post")]
    public async Task TestDeleteAPostController()
    {
    
        var mockHttp = new MockHttpMessageHandler();

        var newPost = JsonConvert.SerializeObject(new
          {
              Content = "Um post qualquer",
              UserId = "53912546-34dc-4c60-75b8-08dab2cc3a7a"
          });

        mockHttp.When(HttpMethod.Post, $"{apiUri}/post")
                .Respond("application/json", newPost);

        mockHttp.When(HttpMethod.Get, $"{apiUri}/post/*")
                .Respond("application/json", newPost);

        mockHttp.When(HttpMethod.Delete, $"{apiUri}/post/*")
                .Respond(System.Net.HttpStatusCode.NoContent);
                
        var client = mockHttp.ToHttpClient();

        var response = await client.PostAsync($"{apiUri}/post", new StringContent(newPost, Encoding.UTF8, "application/json"));
        response.Should().BeSuccessful();

        var getUser = await client.GetAsync($"{apiUri}/post/id");
        getUser.Should().BeSuccessful();

        var deletePost = await client.DeleteAsync($"{apiUri}/post/id");
        deletePost.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);

    }

    [Trait("Post", "2 - Post")]
    [Fact(DisplayName = "Teste para Get Post")]
    public async Task TestGetAPostController()
    {
    
        var mockHttp = new MockHttpMessageHandler();

        var newPost = JsonConvert.SerializeObject(new
          {
              Content = "Um post qualquer",
              UserId = "53912546-34dc-4c60-75b8-08dab2cc3a7a"
          });

        mockHttp.When(HttpMethod.Post, $"{apiUri}/post")
                .Respond("application/json", newPost);

        mockHttp.When(HttpMethod.Get, $"{apiUri}/post/*")
                .Respond("application/json", newPost);
                
        var client = mockHttp.ToHttpClient();

        var response = await client.PostAsync($"{apiUri}/post", new StringContent(newPost, Encoding.UTF8, "application/json"));
        response.Should().BeSuccessful();

        var getPost = await client.GetAsync($"{apiUri}/post/id");
        getPost.Should().BeSuccessful();

        var json = await response.Content.ReadAsStringAsync();
        json.Should().Contain(newPost);

    }

    [Trait("Post", "2 - Post")]
    [Fact(DisplayName = "Teste para Update Post")]
    public async Task TestUpdateAPostController()
    {
    
        var mockHttp = new MockHttpMessageHandler();

        var newPost = JsonConvert.SerializeObject(new
          {
              Content = "Um post qualquer",
              UserId = "53912546-34dc-4c60-75b8-08dab2cc3a7a"
          });

        var updatedPost = JsonConvert.SerializeObject(new
          {
              Content = "Um post ATUALIZADO",
              UserId = "53912546-34dc-4c60-75b8-08dab2cc3a7a"
          });

        mockHttp.When(HttpMethod.Post, $"{apiUri}/post")
                .Respond("application/json", newPost);

        mockHttp.When(HttpMethod.Put, $"{apiUri}/post")
                .Respond("application/json", updatedPost);
                
        var client = mockHttp.ToHttpClient();

        var response = await client.PostAsync($"{apiUri}/post", new StringContent(newPost, Encoding.UTF8, "application/json"));
        response.Should().BeSuccessful();

        var getPost = await client.PutAsync($"{apiUri}/post", new StringContent(updatedPost, Encoding.UTF8, "application/json"));
        getPost.Should().BeSuccessful();

        var json = await getPost.Content.ReadAsStringAsync();
        json.Should().Contain(updatedPost);

    }
}

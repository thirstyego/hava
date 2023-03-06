// using Newtonsoft.Json;
//
//
// using System.Net;
// using System.Net.Http;
// using System.Net.Http.Headers;
// using System.Net.Http.Json;
// using System.Threading.Tasks;
// using hava.Auth;
// using Microsoft.AspNetCore.Mvc.Testing;
// using Xunit;
//
// namespace Testing.AuthenticationController;
//
// public class RegisterControllerTests : IClassFixture<WebApplicationFactory<Program>>
// {
//     private readonly WebApplicationFactory<Program> _factory;
//
//     public RegisterControllerTests(WebApplicationFactory<Program> factory)
//     {
//         _factory = factory;
//     }
//
//     [Fact]
//     public async Task Register_Returns_Success()
//     {
//         // Arrange
//         var client = _factory.CreateClient();
//         var model = new RegisterModel { Email = "uncle@gmail.com", Username = "uncle", Password = "Mehmet88!" };
//
//         // Act
//         var response = await client.PostAsJsonAsync("/api/Authenticate/register", model);
//
//         // Assert
//         response.EnsureSuccessStatusCode();
//         Assert.Equal(MediaTypeHeaderValue.Parse("application/json; charset=utf-8"), response.Content.Headers.ContentType);
//
//         var responseContent = await response.Content.ReadAsStringAsync();
//         Assert.NotNull(responseContent);
//
//     }
// }
//

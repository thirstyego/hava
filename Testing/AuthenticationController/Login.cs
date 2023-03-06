// using Newtonsoft.Json;
//
//
// using System.Net;
// using System.Net.Http;
// using System.Net.Http.Headers;
// using System.Net.Http.Json;
// using System.Threading.Tasks;
// using hava.Auth;
// using hava.Models.Auth;
// using Microsoft.AspNetCore.Mvc.Testing;
// using Xunit;
//
// namespace Testing.AuthenticationController;
//
// public class AuthenticationControllerTests : IClassFixture<WebApplicationFactory<Program>>
// {
//     private readonly WebApplicationFactory<Program> _factory;
//
//     public AuthenticationControllerTests(WebApplicationFactory<Program> factory)
//     {
//         _factory = factory;
//     }
//
//     [Fact]
//     public async Task Login_Returns_Token_For_Valid_Credentials()
//     {
//         // Arrange
//         var client = _factory.CreateClient();
//         var model = new LoginModel { Username = "uncle", Password = "Mehmet88!" };
//
//         // Act
//         var response = await client.PostAsJsonAsync("/api/Authenticate/login", model);
//
//         // Assert
//         response.EnsureSuccessStatusCode();
//         Assert.Equal(MediaTypeHeaderValue.Parse("application/json; charset=utf-8"), response.Content.Headers.ContentType);
//
//         var responseContent = await response.Content.ReadAsStringAsync();
//         Assert.NotNull(responseContent);
//
//         var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseContent);
//         Assert.NotNull(tokenResponse.Token);
//         Assert.NotNull(tokenResponse.Expiration);
//         Assert.NotNull(tokenResponse.Id);
//     }
//
//     [Fact]
//     public async Task Login_Returns_Unauthorized_For_Invalid_Credentials()
//     {
//         // Arrange
//         var client = _factory.CreateClient();
//         var model = new LoginModel { Username = "testuser", Password = "invalidpassword" };
//
//         // Act
//         var response = await client.PostAsJsonAsync("/api/Authenticate/login", model);
//
//         // Assert
//         Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
//     }
// }
//

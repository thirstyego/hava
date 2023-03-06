using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using hava.Auth;
using hava.Models.Auth;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Testing.AuthenticationController;

public class AuthControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public AuthControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Theory]
    [InlineData("uncle@gmail.com", "uncle", "Mehmet88!")]
    [InlineData("aunt@gmail.com", "aunt", "Mehmet88!")]
    public async Task Auth_Returns_Success(string email, string username, string password)
    {
        // Arrange
        var client = _factory.CreateClient();
        var model = new RegisterModel { Email = email, Username = username, Password = password };

        // Act
        var response = await client.PostAsJsonAsync("/api/Authenticate/register", model);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(MediaTypeHeaderValue.Parse("application/json; charset=utf-8"), response.Content.Headers.ContentType);

        var responseContent = await response.Content.ReadAsStringAsync();
        Assert.NotNull(responseContent);

        
        
        
        
        // Arrange
        var loginModel = new LoginModel { Username = username, Password = password };

        // Act
        var loginResponse = await client.PostAsJsonAsync("/api/Authenticate/login", loginModel);

        // Assert
        loginResponse.EnsureSuccessStatusCode();
        Assert.Equal(MediaTypeHeaderValue.Parse("application/json; charset=utf-8"), loginResponse.Content.Headers.ContentType);

        var loginResponseContent = await loginResponse.Content.ReadAsStringAsync();
        Assert.NotNull(loginResponseContent);

        var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(loginResponseContent);
        Assert.NotNull(tokenResponse.Token);
        Assert.NotNull(tokenResponse.Expiration);
        Assert.NotNull(tokenResponse.Id);
    }
}


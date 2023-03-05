using System.Net;
using System.Text;
using System.Text.Json;
using hava.Auth;
using hava.Models.Auth;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Xunit.Abstractions;


namespace Tester;

public class AuthenticateControllerTest : BaseTest
{
    
    public AuthenticateControllerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        
    }

    
    [Theory]
    [InlineData("emre", "emre@yahoo.com", "Mehmet88!")]
    public async void Register_user_test(string username, string email, string password)
    {
        
        var registerUser = new RegisterModel
        {
            Username = username,
            Email = email,
            Password = password
        };
        
        var serRegisterUser = JsonSerializer.Serialize(registerUser);
        var requestContent = new StringContent(
            serRegisterUser, Encoding.UTF8, "application/json");
        
        var client = Create_HttpClient();
        var uri = Path.Combine("Authenticate/register");
        var response = await client.PostAsync(uri, requestContent);
        
        response.EnsureSuccessStatusCode();
    }

    
    [Theory]
    [InlineData("emre", "Mehmet88!")]
    public async void Login_user_test(string username, string password)
    {
        var loginUser = new LoginModel()
        {
            Username = username,
            Password = password
        };
        
        var serLoginUser = JsonSerializer.Serialize(loginUser);
        var requestContent = new StringContent(serLoginUser, Encoding.UTF8, "application/json");

        var client = Create_HttpClient();
        var uri = Path.Combine("Authenticate/login");
        var response = await client.PostAsync(uri, requestContent);
        
        var content = await response.Content.ReadAsStringAsync();
        var _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var loginResponse = JsonSerializer.Deserialize<LoginResponse>(content, _options);

        response.EnsureSuccessStatusCode();
        Assert.IsType<string>(loginResponse.Token);
        Assert.IsType<string>(loginResponse.Id);
    }
}
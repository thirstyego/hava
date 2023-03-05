using System.Text;
using System.Text.Json;
using hava.Models;
using Xunit.Abstractions;

namespace Tester;

public class HomeControllerTest : BaseTest
{

    public HomeControllerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        
    }
    
    
    [Theory]
    [InlineData("ankara")]
    public async void PostHomeTest(string name)
    {
        
        var Home = new HomePost
        {
            Name = name,
            ApplicationUserId = currentUserId
        };
        
        var serializedHome = JsonSerializer.Serialize(Home);
        var requestContent = new StringContent(
            serializedHome, Encoding.UTF8, "application/json");
        
        var client = Create_HttpClient();
        var uri = Path.Combine("Home");
        var response = await client.PostAsync(uri, requestContent);
        
        response.EnsureSuccessStatusCode();
    }
    
    
    [Theory]
    [InlineData(4, "malatya")]
    public async void PutHomeTest(int id, string name)
    {
        var Home = new HomePut
        {
            Id = id,
            Name = name,
            ApplicationUserId = currentUserId
        };
        
        var serializedHome = JsonSerializer.Serialize(Home);
        var requestContent = new StringContent(
            serializedHome, Encoding.UTF8, "application/json");
        
        var client = Create_HttpClient();
        var uri = Path.Combine("Home");
        var response = await client.PutAsync(uri, requestContent);
        
        response.EnsureSuccessStatusCode();
    }
    
    
    [Fact]
    public async void GetUserHomesTest()
    {
        var client = Create_HttpClient();
        var uri = Path.Combine($"Home/user/{currentUserId}");
        var response = await client.GetAsync(uri);
        
        response.EnsureSuccessStatusCode();
    }
    
    [Theory]
    [InlineData(4)]
    public async void GetHomeTest(int id)
    {
        var client = Create_HttpClient();
        var uri = Path.Combine($"Home/{id}");
        var response = await client.GetAsync(uri);
        
        response.EnsureSuccessStatusCode();
    }
    
    [Theory]
    [InlineData(4)]
    public async void DeleteHomeTest(int id)
    {
        var client = Create_HttpClient();
        var uri = Path.Combine($"Home/{id}");
        var response = await client.DeleteAsync(uri);
        
        response.EnsureSuccessStatusCode();
    }
}


// using hava.Controllers;
// using hava.Data;
// using hava.Models;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.DependencyInjection;
//
// namespace MyProject.Tests.Controllers
// {
//     public class HomeControllerTests
//     {
//         private readonly IServiceProvider _serviceProvider;
//         private readonly HomeController _homeController;
//
//         public HomeControllerTests()
//         {
//             // Set up a test database
//             var services = new ServiceCollection();
//             services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("TestDb"));
//             _serviceProvider = services.BuildServiceProvider();
//
//             // Instantiate the home controller
//             var dbContext = _serviceProvider.GetRequiredService<MyDbContext>();
//             _homeController = new HomeController(dbContext);
//         }
//
//         [Fact]
//         public async Task Create_Returns_CreatedAtActionResult()
//         {
//             // Arrange
//             var home = new HomePost { Name = "Test Home", ApplicationUserId = "user1" };
//
//             // Act
//             var result = await _homeController.PostHome(home);
//
//             // Assert
//             var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
//             var createdHome = Assert.IsType<Home>(createdAtActionResult.Value);
//             Assert.Equal(home.Name, createdHome.Name);
//             Assert.Equal(home.ApplicationUserId, createdHome.ApplicationUserId);
//         }
//
//         [Fact]
//         public async Task Read_Returns_OkObjectResult_With_Home()
//         {
//             // Arrange
//             var home = new Home { Name = "Test Home", UserId = "user1" };
//             await _homeController.Create(home);
//
//             // Act
//             var result = await _homeController.Read(home.Id);
//
//             // Assert
//             var okObjectResult = Assert.IsType<OkObjectResult>(result);
//             var returnedHome = Assert.IsType<Home>(okObjectResult.Value);
//             Assert.Equal(home.Id, returnedHome.Id);
//             Assert.Equal(home.Name, returnedHome.Name);
//             Assert.Equal(home.UserId, returnedHome.UserId);
//         }
//
//         [Fact]
//         public async Task Update_Returns_OkResult()
//         {
//             // Arrange
//             var home = new Home { Name = "Test Home", UserId = "user1" };
//             await _homeController.Create(home);
//
//             var updatedHome = new Home { Id = home.Id, Name = "Updated Test Home", UserId = "user2" };
//
//             // Act
//             var result = await _homeController.Update(updatedHome.Id, updatedHome);
//
//             // Assert
//             Assert.IsType<OkResult>(result);
//
//             var dbContext = _serviceProvider.GetRequiredService<MyDbContext>();
//             var dbHome = await dbContext.Homes.FindAsync(home.Id);
//             Assert.Equal(updatedHome.Name, dbHome.Name);
//             Assert.Equal(updatedHome.UserId, dbHome.UserId);
//         }
//
//         [Fact]
//         public async Task Delete_Returns_OkResult()
//         {
//             // Arrange
//             var home = new Home { Name = "Test Home", UserId = "user1" };
//             await _homeController.Create(home);
//
//             // Act
//             var result = await _homeController.Delete(home.Id);
//
//             // Assert
//             Assert.IsType<OkResult>(result);
//
//             var dbContext = _serviceProvider.GetRequiredService<MyDbContext>();
//             var dbHome = await dbContext.Homes.FindAsync(home.Id);
//             Assert.Null(dbHome);
//         }
//     }
// }

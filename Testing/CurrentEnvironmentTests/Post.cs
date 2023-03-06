
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;
using hava.Controllers;
using hava.Data;
using hava.Models;


namespace Testing.CurrentEnvironmentControllerTests;


// Create a test class
public class Post
{
    // Create a test method for the PostDevice action
    [Fact]
    public async Task PostCurrentEnvironment_ReturnsCreated()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "PostCurrentEnvironment_ReturnsCreated")
            .Options;

        var currentEnvironmentPost = new CurrentEnvironmentPost
        {
            Name = "TestEnvironment1", Temperature = "65", TargetTemperature = "67", Humidity = "25",
            TargetHumidity = "30", Mode = Mode.Heat, Date = DateTime.Now.ToString(), ZoneId = 1
        };

        var controller = new CurrentEnvironmentController(new ApplicationDbContext(options));

        // Act
        var result = await controller.PostCurrentEnvironment(currentEnvironmentPost);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnValue = Assert.IsType<CurrentEnvironment>(createdResult.Value);
        Assert.Equal(currentEnvironmentPost.Temperature, returnValue.Temperature);
        Assert.Equal(currentEnvironmentPost.Humidity, returnValue.Humidity);
        Assert.Equal(currentEnvironmentPost.ZoneId, returnValue.ZoneId);
    }

    [Fact]
    public async Task PostCurrentEnvironment_ReturnsBadRequest_WhenModelStateIsInvalid()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "PostCurrentEnvironment_ReturnsBadRequest_WhenModelStateIsInvalid")
            .Options;

        var controller = new CurrentEnvironmentController(new ApplicationDbContext(options));

        // Act
        var result = await controller.PostCurrentEnvironment(null);

        // Assert
        Assert.IsType<BadRequestResult>(result.Result);
    }
}

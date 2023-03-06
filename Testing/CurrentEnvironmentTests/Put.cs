using hava.Controllers;
using hava.Data;
using hava.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Testing.CurrentEnvironmentControllerTests;


// Create a test class
public class CurrentEnvironmentControllerTests
{
    // Create a test method for the PutCurrentEnvironment action
    [Fact]
    public async Task PutCurrentEnvironment_ReturnsNoContent()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "PutCurrentEnvironment_ReturnsNoContent")
            .Options;

        var currentEnvironmentPut = new CurrentEnvironmentPut
        {
            Id = 1, Name = "TestEnvironment1", Temperature = "65", TargetTemperature = "67", Humidity = "25",
            TargetHumidity = "30", Mode = Mode.Heat, Date = DateTime.Now.ToString(), ZoneId = 1
        };

        var controller = new CurrentEnvironmentController(new ApplicationDbContext(options));

        // Act
        var result = await controller.PutCurrentEnvironment(currentEnvironmentPut);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task PutCurrentEnvironment_ReturnsBadRequest_WhenCurrentEnvironmentPutIsNull()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "PutCurrentEnvironment_ReturnsBadRequest_WhenCurrentEnvironmentPutIsNull")
            .Options;
        
        var controller = new CurrentEnvironmentController(new ApplicationDbContext(options));

        // Act
        var result = await controller.PutCurrentEnvironment(null);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task PutCurrentEnvironment_ReturnsNotFound_WhenCurrentEnvironmentDoesNotExist()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "PutCurrentEnvironment_ReturnsNotFound_WhenCurrentEnvironmentDoesNotExist")
            .Options;

        var currentEnvironmentPut = new CurrentEnvironmentPut
        {
            Id = 1, Name = "TestEnvironment1", Temperature = "65", TargetTemperature = "67", Humidity = "25",
            TargetHumidity = "30", Mode = Mode.Heat, Date = DateTime.Now.ToString(), ZoneId = 1
        };

        var controller = new CurrentEnvironmentController(new ApplicationDbContext(options));

        // Act
        var result = await controller.PutCurrentEnvironment(currentEnvironmentPut);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}

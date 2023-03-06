using hava.Controllers;
using hava.Data;
using hava.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace Testing.DeviceControllerTests;


// Create a test class
public class Post
{
    // Create a test method for the PostDevice action
    [Fact]
    public async Task PostDevice_ReturnsCreated()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "PostDevice_ReturnsCreated")
            .Options;

        var devicePost = new DevicePost
        {
            BatteryPercentage = 50,
            Status = true,
            ZoneId = 1
        };

        var controller = new DeviceController(new ApplicationDbContext(options));

        // Act
        var result = await controller.PostDevice(devicePost);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnValue = Assert.IsType<Device>(createdResult.Value);
        Assert.Equal(devicePost.BatteryPercentage, returnValue.BatteryPercentage);
        Assert.Equal(devicePost.Status, returnValue.Status);
        Assert.Equal(devicePost.ZoneId, returnValue.ZoneId);
    }

    [Fact]
    public async Task PostDevice_ReturnsBadRequest_WhenModelStateIsInvalid()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "PostDevice_ReturnsBadRequest_WhenModelStateIsInvalid")
            .Options;

        // var devicePost = new DevicePost();

        var controller = new DeviceController(new ApplicationDbContext(options));
        // controller.ModelState.AddModelError("BatteryPercentage", "The BatteryPercentage field is required.");

        // Act
        var result = await controller.PostDevice(null);

        // Assert
        Assert.IsType<BadRequestResult>(result.Result);
    }
}

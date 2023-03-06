using hava.Controllers;
using hava.Data;
using hava.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace Testing.DeviceControllerTests;


// Create a test class
public class DeviceControllerTests
{
    // Create a test method for the PutDevice action
    [Fact]
    public async Task PutDevice_ReturnsNoContent()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "PutDevice_ReturnsNoContent")
            .Options;

        var devicePut = new DevicePut
        {
            Id = 1,
            BatteryPercentage = 50,
            Status = true,
            ZoneId = 1
        };

        var controller = new DeviceController(new ApplicationDbContext(options));

        // Act
        var result = await controller.PutDevice(devicePut);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task PutDevice_ReturnsBadRequest_WhenDevicePutIsNull()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "PutDevice_ReturnsBadRequest_WhenDevicePutIsNull")
            .Options;

        DevicePut devicePut = null;

        var controller = new DeviceController(new ApplicationDbContext(options));

        // Act
        var result = await controller.PutDevice(devicePut);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task PutDevice_ReturnsNotFound_WhenDeviceDoesNotExist()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "PutDevice_ReturnsNotFound_WhenDeviceDoesNotExist")
            .Options;

        var devicePut = new DevicePut
        {
            Id = 1,
            BatteryPercentage = 50,
            Status = true,
            ZoneId = 1
        };

        var controller = new DeviceController(new ApplicationDbContext(options));

        // Act
        var result = await controller.PutDevice(devicePut);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}

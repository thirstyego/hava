using hava.Controllers;
using hava.Data;
using hava.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Testing.DeviceControllerTests;

public class Delete
{
    private readonly DbContextOptions<ApplicationDbContext> _options;

    public Delete()
    {
        // Set up a test database context and controller instance
        _options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "DeleteDeviceTestDB")
            .Options;
    }

    [Fact]
    public async Task DeleteDevice_ReturnsNotFound_WhenDeviceNotFound()
    {

        using (var context = new ApplicationDbContext(_options))
        {
            var controller = new DeviceController(context);
            
            // Arrange
            var fakeDeviceId = 123;

            // Act
            var result = await controller.DeleteDevice(fakeDeviceId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }

    [Fact]
    public async Task DeleteDevice_RemovesDeviceFromContext()
    {
        using (var context = new ApplicationDbContext(_options))
        {
            var controller = new DeviceController(context);
            
            // Arrange
            var device = new Device { Id = 1 };
            context.Devices.Add(device);
            await context.SaveChangesAsync();

            // Act
            var result = await controller.DeleteDevice(device.Id);

            // Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Null(await context.Devices.FindAsync(device.Id));
        }
    }
}

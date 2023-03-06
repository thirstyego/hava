using hava.Controllers;
using hava.Data;
using hava.Models;

namespace Testing.DeviceControllerTests;

// Import necessary namespaces
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

// Create a test class
public class Get
{
    
    
    // Create a test method for the GetDevices action
    [Fact]
    public async Task GetDevices_ReturnsDevicesInZone()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "GetDevices_ReturnsDevicesInZone")
            .Options;

        // Initialize some test data
        var devices = new List<Device>
        {
            new Device { Id = 1, Status = true, ZoneId = 1, BatteryPercentage = 45 },
            new Device { Id = 2, Status = true, ZoneId = 2, BatteryPercentage = 60 },
            new Device { Id = 3, Status = true, ZoneId = 1, BatteryPercentage = 20 },
        };
        using (var context = new ApplicationDbContext(options))
        {
            await context.Devices.AddRangeAsync(devices);
            await context.SaveChangesAsync();
        }

        var controller = new DeviceController(new ApplicationDbContext(options));

        // Act
        var result = await controller.GetDevices(1);

        // Assert
        // var okResult = Assert.IsType<ActionResult<IEnumerable<Device>>>(result);
        // var devicesInZone = Assert.IsAssignableFrom<IEnumerable<Device>>(okResult.Value);
        // Assert.Equal(2, devicesInZone.Count());
        // Assert.Contains(devices[], devicesInZone);
        // Assert.Contains(devices[2], devicesInZone);
        
        // Assert
        var zonesResult = result.Value.ToList();
        Assert.Equal(2, zonesResult.Count);
        Assert.Contains(zonesResult, h => h.Id == 1);
        Assert.Contains(zonesResult, h => h.Id == 3);
    }
    
    
    

        // Create a test class
        // Create a test method for the GetDevice action
        [Fact]
        public async Task GetDevice_ReturnsDevice()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetDevice_ReturnsDevice")
                .Options;

            // Initialize some test data
            var device = new Device { Id = 1, Status = true, ZoneId = 1, BatteryPercentage = 50 };
            using (var context = new ApplicationDbContext(options))
            {
                await context.Devices.AddAsync(device);
                await context.SaveChangesAsync();
            }

            var controller = new DeviceController(new ApplicationDbContext(options));

            // Act
            var result = await controller.GetDevice(1);

            // Assert
            var okResult = Assert.IsType<ActionResult<Device>>(result);
            var returnedDevice = Assert.IsType<Device>(okResult.Value);
            Assert.Equal(device.Id, returnedDevice.Id);
            Assert.Equal(device.Status, returnedDevice.Status);
            Assert.Equal(device.ZoneId, returnedDevice.ZoneId);
        }

        [Fact]
        public async Task GetDevice_ReturnsNotFound_WhenDeviceNotFound()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetDevice_ReturnsNotFound_WhenDeviceNotFound")
                .Options;

            var controller = new DeviceController(new ApplicationDbContext(options));

            // Act
            var result = await controller.GetDevice(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
 
}

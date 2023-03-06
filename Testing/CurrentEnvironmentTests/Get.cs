using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hava.Controllers;
using hava.Data;
using hava.Models;
using Xunit;

namespace Testing.CurrentEnvironmentControllerTests;

// Create a test class
public class Get
{
    
    // Create a test method for the GetCurrentEnvironment action
    [Fact]
    public async Task GetCurrentEnvironments_ReturnsCurrentEnvironmentsInZone()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "GetCurrentEnvironments_ReturnsCurrentEnvironmentsInZone")
            .Options;

        // Initialize some test data
        var zoneId = 1;
        var currentEnvironments = new List<CurrentEnvironment>
        {
            new CurrentEnvironment { Id = 1, Name = "TestEnvironment1", Temperature = "65", TargetTemperature = "67", Humidity = "25", TargetHumidity = "30", Mode = Mode.Heat, Date = DateTime.Now.ToString(), ZoneId = zoneId },
            new CurrentEnvironment { Id = 2, Name = "TestEnvironment2", Temperature = "65", TargetTemperature = "67", Humidity = "25", TargetHumidity = "30", Mode = Mode.Heat, Date = DateTime.Now.ToString(), ZoneId = 2 },
            new CurrentEnvironment { Id = 3, Name = "TestEnvironment3", Temperature = "65", TargetTemperature = "67", Humidity = "25", TargetHumidity = "30", Mode = Mode.Heat, Date = DateTime.Now.ToString(), ZoneId = zoneId }
        };
        using (var context = new ApplicationDbContext(options))
        {
            await context.CurrentEnvironments.AddRangeAsync(currentEnvironments);
            await context.SaveChangesAsync();
        }

        var controller = new CurrentEnvironmentController(new ApplicationDbContext(options));

        // Act
        var result = await controller.GetCurrentEnvironments(1);

        // Assert
        var zonesResult = result.Value.ToList();
        Assert.Equal(2, zonesResult.Count);
        Assert.Contains(zonesResult, h => h.Id == 1);
        Assert.Contains(zonesResult, h => h.Id == 3);
    }
    
    
    

        // Create a test class
        // Create a test method for the GetCurrentEnvironment action
        [Fact]
        public async Task GetCurrentEnvironment_ReturnsCurrentEnvironment()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetCurrentEnvironment_ReturnsCurrentEnvironment")
                .Options;

            // Initialize some test data

            var zoneId = 1;
            var currentEnvironment = new CurrentEnvironment
            {
                Id = 1, Name = "TestEnvironment1", Temperature = "65", TargetTemperature = "67", Humidity = "25",
                TargetHumidity = "30", Mode = Mode.Heat, Date = DateTime.Now.ToString(), ZoneId = zoneId
            };
            using (var context = new ApplicationDbContext(options))
            {
                await context.CurrentEnvironments.AddAsync(currentEnvironment);
                await context.SaveChangesAsync();
            }

            var controller = new CurrentEnvironmentController(new ApplicationDbContext(options));

            // Act
            var result = await controller.GetCurrentEnvironment(1);

            // Assert
            var okResult = Assert.IsType<ActionResult<CurrentEnvironment>>(result);
            var returnedCurrentEnvironment = Assert.IsType<CurrentEnvironment>(okResult.Value);
            Assert.Equal(currentEnvironment.Id, returnedCurrentEnvironment.Id);
            Assert.Equal(currentEnvironment.ZoneId, returnedCurrentEnvironment.ZoneId);
        }

        [Fact]
        public async Task GetCurrentEnvironment_ReturnsNotFound_WhenCurrentEnvironmentNotFound()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetCurrentEnvironment_ReturnsNotFound_WhenCurrentEnvironmentNotFound")
                .Options;

            var controller = new CurrentEnvironmentController(new ApplicationDbContext(options));

            // Act
            var result = await controller.GetCurrentEnvironment(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
 
}

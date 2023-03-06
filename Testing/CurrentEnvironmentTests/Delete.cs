using hava.Controllers;
using hava.Data;
using hava.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Testing.CurrentEnvironmentControllerTests;

public class Delete
{
    private readonly DbContextOptions<ApplicationDbContext> _options;

    public Delete()
    {
        // Set up a test database context and controller instance
        _options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "DeleteCurrentEnvironmentTestDB")
            .Options;
    }

    [Fact]
    public async Task DeleteCurrentEnvironment_ReturnsNotFound_WhenCurrentEnvironmentNotFound()
    {

        using (var context = new ApplicationDbContext(_options))
        {
            var controller = new CurrentEnvironmentController(context);
            
            // Arrange
            var fakeCurrentEnvironmentId = 123;

            // Act
            var result = await controller.DeleteCurrentEnvironment(fakeCurrentEnvironmentId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }

    [Fact]
    public async Task DeleteCurrentEnvironment_RemovesCurrentEnvironmentFromContext()
    {
        using (var context = new ApplicationDbContext(_options))
        {
            var controller = new CurrentEnvironmentController(context);
            
            // Arrange
            
            var currentEnvironment = new CurrentEnvironment
            {
                Id = 1, Name = "TestEnvironment1", Temperature = "65", TargetTemperature = "67", Humidity = "25",
                TargetHumidity = "30", Mode = Mode.Heat, Date = DateTime.Now.ToString(), ZoneId = 1
            };
            
            context.CurrentEnvironments.Add(currentEnvironment);
            await context.SaveChangesAsync();

            // Act
            var result = await controller.DeleteCurrentEnvironment(currentEnvironment.Id);

            // Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Null(await context.CurrentEnvironments.FindAsync(currentEnvironment.Id));
        }
    }
}

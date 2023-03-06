using hava.Controllers;
using hava.Data;
using hava.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Testing.ZoneControllerTests;

public class Delete
{
    private readonly DbContextOptions<ApplicationDbContext> _options = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase(databaseName: "DeleteZoneTestDB")
        .Options;

    
    // [Fact]
    // public async Task DeleteZone_ReturnsBadRequest_WhenZoneIsNull()
    // {
    //     // Arrange
    //     using (var context = new ApplicationDbContext(_options))
    //     {
    //         var controller = new ZoneController(context);
    //
    //         // Act
    //         var result = await controller.DeleteZone(null);
    //
    //         // Assert
    //         Assert.IsType<BadRequestResult>(result.Result);
    //     }
    // }
    
    [Fact]
    public async Task DeleteZone_ReturnsNotFound_WhenZoneDoesNotExist()
    {
        // Arrange
        using (var context = new ApplicationDbContext(_options))
        {
            var controller = new ZoneController(context);

            // Act
            var result = await controller.DeleteZone(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }

    [Fact]
    public async Task DeleteZone_RemovesZoneFromDatabase()
    {
        // Arrange
        using (var context = new ApplicationDbContext(_options))
        {
            context.Zones.Add(new Zone { Id = 1, Name = "Zone 1" });
            context.SaveChanges();

            var controller = new ZoneController(context);

            // Act
            var result = await controller.DeleteZone(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Null(context.Zones.Find(1));
        }
    }
}

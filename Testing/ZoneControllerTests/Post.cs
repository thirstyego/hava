using hava.Controllers;
using hava.Data;
using hava.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Testing.ZoneControllerTests;

public class Post
{
    private readonly DbContextOptions<ApplicationDbContext> _options = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase(databaseName: "PostZoneTestDB")
        .Options;

    [Fact]
    public async Task PostZone_ReturnsBadRequest_WhenZonePostIsNull()
    {
        // Arrange
        using (var context = new ApplicationDbContext(_options))
        {
            var controller = new ZoneController(context);

            // Act
            var result = await controller.PostZone(null);

            // Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }
    }

    [Fact]
    public async Task PostZone_CreatesNewZone()
    {
        // Arrange
        using (var context = new ApplicationDbContext(_options))
        {
            var controller = new ZoneController(context);

            var zonePost = new ZonePost
            {
                Name = "Zone 1",
                HomeId = 1
            };

            // Act
            var result = await controller.PostZone(zonePost);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var zone = Assert.IsType<Zone>(createdResult.Value);

            Assert.Equal("Zone 1", zone.Name);
            Assert.Equal(1, zone.HomeId);

            var zoneInDb = await context.Zones.FindAsync(zone.Id);
            Assert.NotNull(zoneInDb);
            Assert.Equal("Zone 1", zoneInDb.Name);
            Assert.Equal(1, zoneInDb.HomeId);
        }
    }
}

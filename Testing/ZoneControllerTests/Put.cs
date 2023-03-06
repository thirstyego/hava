using hava.Controllers;
using hava.Data;
using hava.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Testing.ZoneControllerTests;

public class Put
{
    private readonly DbContextOptions<ApplicationDbContext> _options = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase(databaseName: "PutZoneTestDB")
        .Options;

    [Fact]
    public async Task PutZone_ReturnsBadRequest_WhenZonePutIsNull()
    {
        // Arrange
        using (var context = new ApplicationDbContext(_options))
        {
            var controller = new ZoneController(context);

            // Act
            var result = await controller.PutZone(null);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }

    [Fact]
    public async Task PutZone_ReturnsNotFound_WhenZoneIdDoesNotExist()
    {
        // Arrange
        using (var context = new ApplicationDbContext(_options))
        {
            var controller = new ZoneController(context);

            var zonePut = new ZonePut
            {
                Id = 4,
                Name = "Zone 1",
                HomeId = 1
            };

            // Act
            var result = await controller.PutZone(zonePut);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }

    [Fact]
    public async Task PutZone_UpdatesExistingZone()
    {

        int zoneId;
        
        using (var context = new ApplicationDbContext(_options))
        {
            var zone = new Zone
            {
                Name = "Zone 1",
                HomeId = 1
            };

            context.Zones.Add(zone);
            await context.SaveChangesAsync();

            zoneId = zone.Id;
        }

        using (var context = new ApplicationDbContext(_options))
        {
            var controller = new ZoneController(context);

            var zonePut = new ZonePut
            {
                Id = zoneId,
                Name = "Zone 2",
                HomeId = 2
            };

            // Act
            var result = await controller.PutZone(zonePut);

            // Assert
            Assert.IsType<NoContentResult>(result);

            var zoneInDb = await context.Zones.FindAsync(zoneId);
            Assert.NotNull(zoneInDb);
            Assert.Equal("Zone 2", zoneInDb.Name);
            Assert.Equal(2, zoneInDb.HomeId);
        }
    }
}

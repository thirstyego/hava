using hava.Controllers;
using hava.Data;
using hava.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Testing.ZoneControllerTests;

public class Get
{
    
        private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

        public Get()
        {
            // Set up an in-memory database for testing
            _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetZoneTestDB")
                .Options;
        }
    
        [Fact]
        public async Task GetZones_Returns_HomeZones()
        {
            
            // Arrange
            var homeId = 1;
            var zones = new List<Zone>
            {
                new Zone { Id = 1, Name = "kitchen", HomeId = homeId },
                new Zone { Id = 2, Name = "living room", HomeId = 2 },
                new Zone { Id = 3, Name = "master bedroom", HomeId = homeId },
            }.AsQueryable();

            // Use an in-memory database for testing
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetZones_Returns_HomeZones")
                .Options;
            
            var dbContext = new ApplicationDbContext(options);
            dbContext.Zones.AddRange(zones);
            await dbContext.SaveChangesAsync();

            // Create the controller
            var controller = new ZoneController(dbContext);

            // Act
            var result = await controller.GetZones(homeId);

            // Assert
            var homesResult = result.Value.ToList();
            Assert.Equal(2, homesResult.Count);
            Assert.Contains(homesResult, h => h.Id == 1);
            Assert.Contains(homesResult, h => h.Id == 3);
        }

        [Theory]
        [InlineData(11, "Zone 1", 1)]
        public async Task GetZone_ReturnsZoneGet_WhenZoneFound(int id, string name, int homeId)
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetZone_ReturnsZoneGet_WhenZoneFound")
                .Options;
            
            using (var context = new ApplicationDbContext(options))
            {
                context.Zones.Add(new Zone { Id = id, Name = name, HomeId  = homeId });
                await context.SaveChangesAsync();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var controller = new ZoneController(context);

                // Act
                var result = await controller.GetZone(id);

                // Assert
                Assert.IsType<Zone>(result.Value);
                Assert.Equal(id, result.Value.Id);
                Assert.Equal(name, result.Value.Name);
                Assert.Equal(homeId, result.Value.HomeId);
            }
        }
        
        
        [Theory]
        [InlineData(20, "Home 1", 1)]
        public async Task GetZone_ReturnsNotFound_WhenZoneNotFound(int id, string name, int homeId)
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetZone_ReturnsNotFound_WhenZoneNotFound")
                .Options;
            
            var zone = new Zone { Id = id, Name = name, HomeId = homeId };
            using (var context = new ApplicationDbContext(options))
            {
                context.Zones.Add(zone);
                await context.SaveChangesAsync();
            }
            using (var context = new ApplicationDbContext(options))
            {
                var controller = new ZoneController(context);

                // Act
                var result = await controller.GetZone(2);

                // Assert
                Assert.IsType<NotFoundResult>(result.Result);
            }
        }
}

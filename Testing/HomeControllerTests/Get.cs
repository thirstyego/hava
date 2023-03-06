using hava.Controllers;
using hava.Data;
using hava.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Testing.HomeControllerTests;

public class Get
{
    
        private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

        public Get()
        {
            // Set up an in-memory database for testing
            _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetHomeTestDB")
                .Options;
        }
    
        [Fact]
        public async Task GetHomes_Returns_UserHomes()
        {
            // Arrange
            var userId = "someUserId";
            var homes = new List<Home>
            {
                new Home { Id = 10, Name = "fox", Date = DateTime.Now.ToString(), ApplicationUserId = userId },
                new Home { Id = 20, Name = "lafayette", Date = DateTime.Now.ToString(), ApplicationUserId = "otherUserId" },
                new Home { Id = 30, Name = "roseville", Date = DateTime.Now.ToString(), ApplicationUserId = userId }
            }.AsQueryable();

            // Use an in-memory database for testing
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Homes.AddRange(homes);
            await dbContext.SaveChangesAsync();

            // Create the controller
            var controller = new HomeController(dbContext);

            // Act
            var result = await controller.GetHomes(userId);

            // Assert
            var homesResult = result.Value.ToList();
            Assert.Equal(2, homesResult.Count);
            Assert.Contains(homesResult, h => h.Id == 10);
            Assert.Contains(homesResult, h => h.Id == 30);
        }

        [Theory]
        [InlineData(1, "Home 1", "ffda1n")]
        public async Task GetHome_ReturnsHomeGet_WhenHomeFound(int id, string name, string applicationUserId)
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetHome_ReturnsHomeGet_WhenHomeFound")
                .Options;
            
            var date = DateTime.Now.ToString();
            using (var context = new ApplicationDbContext(options))
            {
                context.Homes.Add(new Home { Id = id, Name = name, Date = date, ApplicationUserId  = applicationUserId });
                await context.SaveChangesAsync();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var controller = new HomeController(context);

                // Act
                var result = await controller.GetHome(1);

                // Assert
                Assert.IsType<HomeGet>(result.Value);
                Assert.Equal(id, result.Value.Id);
                Assert.Equal(name, result.Value.Name);
                Assert.Equal(date, result.Value.Date);
                Assert.Equal(applicationUserId, result.Value.ApplicationUserId);
            }
        }
        
        
        [Fact]
        public async Task GetHome_ReturnsNotFound_WhenHomeNotFound()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetHome_ReturnsNotFound_WhenHomeNotFound")
                .Options;
            
            var id = 1;
            var name = "Home 1";
            var date = DateTime.Now.ToString();
            var applicationUserId = "ffda1n";
            
            using (var context = new ApplicationDbContext(options))
            {
                context.Homes.Add(new Home { Id = id, Name = name, Date = date, ApplicationUserId  = applicationUserId });
                await context.SaveChangesAsync();
            }
            using (var context = new ApplicationDbContext(options))
            {
                var controller = new HomeController(context);

                // Act
                var result = await controller.GetHome(2);

                // Assert
                Assert.IsType<NotFoundResult>(result.Result);
            }
        }
}
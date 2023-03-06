using hava.Controllers;
using hava.Data;
using hava.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Testing.HomeControllerTests;

public class Delete
{
    
        private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

        public Delete()
        {
            // Set up an in-memory database for testing
            _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "test")
                .Options;
        }
        
        
        [Theory]
        [InlineData(1)]
        public async Task DeleteHome_ReturnsNotFound_WhenHomeNotFound(int id)
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "DeleteHome_ReturnsNotFound_WhenHomeNotFound")
                .Options;
            using (var context = new ApplicationDbContext(options))
            {
                var controller = new HomeController(context);

                // Act
                var result = await controller.DeleteHome(id);

                // Assert
                Assert.IsType<NotFoundResult>(result);
            }
        } 
        
        [Theory]
        [InlineData(1, "Home 1", "ffda1n")]
        public async Task DeleteHome_RemovesHomeAndReturnsNoContent_WhenHomeFound(int id, string name, string applicationUserId)
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "DeleteHome_RemovesHomeAndReturnsNoContent_WhenHomeFound")
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
                var result = await controller.DeleteHome(1);

                // Assert
                Assert.IsType<NoContentResult>(result);
                Assert.Null(await context.Homes.FindAsync(1));
            }
        }
}
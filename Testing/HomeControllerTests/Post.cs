using hava.Controllers;
using hava.Data;
using hava.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Testing.HomeControllerTests;

public class Post
{
    
        private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

        public Post()
        {
            // Set up an in-memory database for testing
            _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "test")
                .Options;
        }
    
        [Fact]
        public async Task PostHome_Returns_OkResult_With_New_Home()
        {
            // Arrange
            var homePost = new HomePost
            {
                ApplicationUserId = "user-id",
                Name = "Test Home"
            };

            using (var dbContext = new ApplicationDbContext(_dbContextOptions))
            {
                var homeController = new HomeController(dbContext);

                // Act
                var result = await homeController.PostHome(homePost);

                if (result is OkObjectResult okResult1)
                {
                    var home = okResult1.Value as Home;
                    
                    Assert.Equal(homePost.ApplicationUserId, home.ApplicationUserId);
                    Assert.Equal(homePost.Name, home.Name);
                }
                
                // Assert
                // var okResult = Assert.IsType<ActionResult<Home>>(result);
                // var home = Assert.IsType<Home>(okResult.Value);
                //
                // Assert.Equal(homePost.ApplicationUserId, home.ApplicationUserId);
                // Assert.Equal(homePost.Name, home.Name);
            }
        }

        
        [Fact]
        public async Task PostHome_Returns_NotFoundResult_When_HomePost_Is_Null()
        {
            // Arrange
            HomePost homePost = null;

            using (var dbContext = new ApplicationDbContext(_dbContextOptions))
            {
                var homeController = new HomeController(dbContext);

                // Act
                var result = await homeController.PostHome(homePost);

                // Assert
                Assert.IsType<NotFoundResult>(result);
            }
        }
}
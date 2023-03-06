using hava.Controllers;
using hava.Data;
using hava.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Testing.HomeControllerTests;

// Import required namespaces
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

// Define the test class
public class Put
{
    
        private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

        public Put()
        {
            // Set up an in-memory database for testing
            _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "PutControllerTestDB")
                .Options;
        }
        
    // Define a test method for PutHome method with valid input
    [Theory]
    [InlineData(1, "some-user-id", "some-name")]
    public async Task PutHome_Returns_NoContentResult_With_Valid_Input(int id, string applicationUserId, string name)
    {
        // Arrange
        var homePut = new HomePut
        {
            Id = id,
            ApplicationUserId = applicationUserId,
            Name = name
        };

        var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "PutHome_Returns_NoContentResult_With_Valid_Input")
            .Options;

        using (var dbContext = new ApplicationDbContext(dbContextOptions))
        {
            dbContext.Homes.Add(new Home
                { Id = id, ApplicationUserId = applicationUserId, Name = name, Date = DateTime.Now.ToString() });
            dbContext.SaveChanges();
        }

        using (var dbContext = new ApplicationDbContext(dbContextOptions))
        {
            var controller = new HomeController(dbContext);

            // Act
            var result = await controller.PutHome(homePut);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }

    // Define a test method for PutHome method with null input
    [Fact]
    public async Task PutHome_Returns_BadRequestResult_With_Null_Input()
    {
        // Arrange
        HomePut homePut = null;

        var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using (var dbContext = new ApplicationDbContext(dbContextOptions))
        {
            var controller = new HomeController(dbContext);

            // Act
            var result = await controller.PutHome(homePut);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }

    // Define a test method for PutHome method with invalid input
    [Theory]
    [InlineData(1, "some-user-id", "some-name", 2)]
    public async Task PutHome_Returns_NotFoundResult_With_Invalid_Input(int id, string applicationUserId, string name, int nonExistantId)
    {
        // Arrange
        var homePut = new HomePut
        {
            Id = nonExistantId,
            ApplicationUserId = applicationUserId,
            Name = name
        };

        var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using (var dbContext = new ApplicationDbContext(dbContextOptions))
        {
            dbContext.Homes.Add(new Home { Id = id, ApplicationUserId = applicationUserId, Name = name, Date = DateTime.Now.ToString() });
            dbContext.SaveChanges();

            var controller = new HomeController(dbContext);

            // Act
            var result = await controller.PutHome(homePut);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}

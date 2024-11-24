using Moq;
using VideoGameCatalogueApi.Controllers;
using VideoGameCatalogueApi.Models;
using VideoGameCatalogueApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace VideoGameCatalogueApi.Tests
{
    public class VideoGameControllerTests
    {
        private readonly Mock<IVideoGameRepository> _mockRepository;
        private readonly VideoGameController _controller;

        public VideoGameControllerTests()
        {
            _mockRepository = new Mock<IVideoGameRepository>();
            _controller = new VideoGameController(_mockRepository.Object);
        }

        // Test case for GetVideoGames action method
        [Fact]
        public async Task GetVideoGames_ReturnsOkResult_WithListOfGames()
        {
            // Arrange: Prepare a list of video games to return from the mock repository
            var games = new List<VideoGame>
            {
                new VideoGame { Id = 1, Title = "Game 1", Genre = 1, Rating = 4.5M, Price = 59.99M, ReleaseDate = DateTime.Now },
                new VideoGame { Id = 2, Title = "Game 2", Genre = 2, Rating = 4.0M, Price = 49.99M, ReleaseDate = DateTime.Now }
            };

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(games);

            // Act: Call the GetVideoGames action method
            var result = await _controller.GetVideoGames();

            // Assert: Verify that the result is of type OkObjectResult and contains the expected games
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnGames = Assert.IsAssignableFrom<IEnumerable<VideoGame>>(okResult.Value);
            Assert.Equal(2, returnGames.Count());
        }

        // Test case for GetVideoGame action method by ID
        [Fact]
        public async Task GetVideoGame_ReturnsOkResult_WithGame()
        {
            // Arrange: Prepare a game with a specific ID to return from the mock repository
            var gameId = 1;
            var game = new VideoGame { Id = gameId, Title = "Game 1", Genre = 1, Rating = 4.5M, Price = 59.99M, ReleaseDate = DateTime.Now };

            _mockRepository.Setup(repo => repo.GetByIdAsync(gameId)).ReturnsAsync(game);

            // Act: Call the GetVideoGame action method by ID
            var result = await _controller.GetVideoGame(gameId);

            // Assert: Verify that the result is of type OkObjectResult and contains the correct game
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnGame = Assert.IsType<VideoGame>(okResult.Value);
            Assert.Equal(gameId, returnGame.Id);
        }

        // Test case for PostVideoGame action method (creating a new game)
        [Fact]
        public async Task PostVideoGame_CreatesGame_AndReturnsCreatedAtActionResult()
        {
            // Arrange: Prepare a new game to be created
            var newGame = new VideoGame
            {
                Id = 3,
                Title = "Game 3",
                Genre = 1,
                Rating = 4.0M,
                Price = 39.99M,
                ReleaseDate = DateTime.Now
            };

            _mockRepository.Setup(repo => repo.AddAsync(It.IsAny<VideoGame>())).Returns(Task.CompletedTask); // Mock method call

            // Act: Call the PostVideoGame action method to create the new game
            var result = await _controller.PostVideoGame(newGame);

            // Assert: Verify that the result is of type CreatedAtActionResult and contains correct details
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetVideoGame", createdResult.ActionName);
            Assert.Equal(newGame.Id, createdResult.RouteValues["id"]);
        }

        // Test case for PutVideoGame action method (updating an existing game)
        [Fact]
        public async Task PutVideoGame_ReturnsNoContent_WhenGameIsUpdated()
        {
            // Arrange: Prepare an updated game to be passed into the method
            var gameId = 1;
            var updatedGame = new VideoGame
            {
                Id = gameId,
                Title = "Updated Game",
                Genre = 1,
                Rating = 4.5M,
                Price = 59.99M,
                ReleaseDate = DateTime.Now
            };

            _mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<VideoGame>())).Returns(Task.CompletedTask);

            // Act: Call the PutVideoGame action method to update the game
            var result = await _controller.PutVideoGame(gameId, updatedGame);

            // Assert: Verify that the result is of type NoContentResult (successful update without returning content)
            Assert.IsType<NoContentResult>(result);
        }

        // Test case for DeleteVideoGame action method (deleting an existing game)
        [Fact]
        public async Task DeleteVideoGame_ReturnsNoContent_WhenGameIsDeleted()
        {
            // Arrange: Prepare a game ID to be deleted
            var gameId = 1;
            _mockRepository.Setup(repo => repo.DeleteAsync(gameId)).Returns(Task.CompletedTask);

            // Act: Call the DeleteVideoGame action method to delete the game
            var result = await _controller.DeleteVideoGame(gameId);

            // Assert: Verify that the result is of type NoContentResult (successful deletion without returning content)
            Assert.IsType<NoContentResult>(result);
        }

        // Test case for PostVideoGame action method (when repository fails)
        [Fact]
        public async Task PostVideoGame_ReturnsBadRequest_WhenRepositoryFails()
        {
            // Arrange: Prepare a new game with a simulated repository failure
            var newGame = new VideoGame
            {
                Id = 3,
                Title = "Game 3",
                Genre = 1,
                Rating = 4.0M,
                Price = 39.99M,
                ReleaseDate = DateTime.Now
            };

            _mockRepository.Setup(repo => repo.AddAsync(It.IsAny<VideoGame>())).Returns(Task.FromException(new Exception("Database Error")));

            // Act: Call the PostVideoGame action method
            var result = await _controller.PostVideoGame(newGame);

            // Assert: Verify that the result is an ObjectResult indicating an error
            Assert.IsType<ObjectResult>(result);
        }

        // Test case for PutVideoGame action method (when update fails)
        [Fact]
        public async Task PutVideoGame_ReturnsBadRequest_WhenUpdateFails()
        {
            // Arrange: Prepare an updated game with a simulated repository failure
            var gameId = 1;
            var updatedGame = new VideoGame
            {
                Id = gameId,
                Title = "Updated Game",
                Genre = 1,
                Rating = 4.5M,
                Price = 59.99M,
                ReleaseDate = DateTime.Now
            };
            _mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<VideoGame>())).Returns(Task.FromException(new Exception("Database Error")));

            // Act: Call the PutVideoGame action method
            var result = await _controller.PutVideoGame(gameId, updatedGame);

            // Assert: Verify that the result is an ObjectResult indicating an error
            Assert.IsType<ObjectResult>(result);
        }

        // Test case for GetVideoGame action method when game does not exist
        [Fact]
        public async Task GetVideoGame_ReturnsNotFound_WhenGameDoesNotExistInDatabase()
        {
            // Arrange: Prepare a non-existing game ID
            var gameId = 100;

            _mockRepository.Setup(repo => repo.GetByIdAsync(gameId)).ReturnsAsync((VideoGame)null);

            // Act: Call the GetVideoGame action method
            var result = await _controller.GetVideoGame(gameId);

            // Assert: Verify that the result is of type NotFoundObjectResult
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        // Test case for PostVideoGame action method when model is invalid
        [Fact]
        public async Task PostVideoGame_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange: Prepare an invalid game model with missing/invalid fields
            var invalidGame = new VideoGame
            {
                Title = "",
                Genre = 1,
                Rating = 6,
                Price = -5,
                ReleaseDate = DateTime.Now
            };
            _controller.ModelState.AddModelError("Title", "The Title field is required.");
            _controller.ModelState.AddModelError("Rating", "The field Rating must be between 0 and 5.");
            _controller.ModelState.AddModelError("Price", "The field Price must be a positive value.");

            // Act: Call the PostVideoGame action method with invalid data
            var result = await _controller.PostVideoGame(invalidGame);

            // Assert: Verify that the result is a BadRequestObjectResult with validation errors
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var modelState = Assert.IsType<SerializableError>(badRequestResult.Value);
            Assert.Contains("The Title field is required.", ((string[])modelState["Title"])[0]);
            Assert.Contains("The field Rating must be between 0 and 5.", ((string[])modelState["Rating"])[0]);
            Assert.Contains("The field Price must be a positive value.", ((string[])modelState["Price"])[0]);
        }
    }
}

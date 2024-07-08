using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MontyHallApi.Controllers;
using MontyHallApi.Model;
using Moq;

namespace MontyHallApi.Tests
{
    [TestFixture]
    public class GameControllerTests
    {
        private GameController _controller;
        private Mock<ILogger<GameController>> _loggerMock;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<GameController>>();
            _controller = new GameController(_loggerMock.Object);
        }

        [Test]
        public void StartGame_ShouldReturnGameId()
        {
            // Act
            var result = _controller.StartGame() as OkObjectResult;
            var value = result.Value as dynamic;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.NotNull(value.GameId);
        }

        [Test]
        public void PickDoor_InvalidGameId_ShouldReturnBadRequest()
        {
            // Arrange
            var request = new PickDoorRequest { GameId = "invalid-id", Door = 1 };

            // Act
            var result = _controller.PickDoor(request) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual("Invalid Game ID", result.Value);
        }

        [Test]
        public void PickDoor_ValidRequest_ShouldReturnRevealedGoatDoor()
        {
            // Arrange
            var gameId = "test-game-id";
            var gameState = new GameState { CarDoor = 1, PickedDoor = -1, RevealedGoatDoor = -1 };
            typeof(GameController).GetField("games", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic)
                .SetValue(null, new Dictionary<string, GameState> { { gameId, gameState } });
            var request = new PickDoorRequest { GameId = gameId, Door = 0 };

            // Act
            var result = _controller.PickDoor(request) as OkObjectResult;
            var value = result.Value as dynamic;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.That((int)value.RevealedGoatDoor, Is.InRange(0, 2));
            Assert.AreNotEqual(gameState.CarDoor, (int)value.RevealedGoatDoor);
            Assert.AreNotEqual(request.Door, (int)value.RevealedGoatDoor);
        }

        [Test]
        public void MakeFinalChoice_InvalidGameId_ShouldReturnBadRequest()
        {
            // Arrange
            var request = new FinalChoiceRequest { GameId = "invalid-id", Switch = true };

            // Act
            var result = _controller.MakeFinalChoice(request) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual("Invalid Game ID", result.Value);
        }

        [Test]
        public void MakeFinalChoice_ValidRequest_ShouldReturnWinResult()
        {
            // Arrange
            var gameId = "test-game-id";
            var gameState = new GameState { CarDoor = 1, PickedDoor = 0, RevealedGoatDoor = 2 };
            typeof(GameController).GetField("games", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic)
                .SetValue(null, new Dictionary<string, GameState> { { gameId, gameState } });
            var request = new FinalChoiceRequest { GameId = gameId, Switch = true };

            // Act
            var result = _controller.MakeFinalChoice(request) as OkObjectResult;
            var value = result.Value as dynamic;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(true, (bool)value.Win);
            Assert.AreEqual(gameState.CarDoor, (int)value.CarDoor);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using MontyHallApi.Model;

namespace MontyHallApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly ILogger<GameController> _logger;
        private static Dictionary<string, GameState> games = new Dictionary<string, GameState>();
        private static Random random = new Random();
        public GameController(ILogger<GameController> logger)
        {
            _logger = logger;
        }


        [HttpPost("StartGame")]
        public IActionResult StartGame()
        {
            try
            {
                string gameId = Guid.NewGuid().ToString();
                int carDoor = random.Next(3);
                games[gameId] = new GameState { CarDoor = carDoor, PickedDoor = -1, RevealedGoatDoor = -1 };
                _logger.LogInformation("Game started with GameId: {GameId}, CarDoor: {CarDoor}", gameId, carDoor);
                return Ok(new { GameId = gameId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while starting the game.");
                return StatusCode(500, new { Message = "An error occurred while starting the game.", Details = ex.Message });
            }
        }

        [HttpPost("PickDoor")]
        public IActionResult PickDoor([FromBody] PickDoorRequest request)
        {
            try
            {
                if (!games.ContainsKey(request.GameId))
                {
                    _logger.LogWarning("Invalid Game ID: {GameId}", request.GameId);
                    return BadRequest("Invalid Game ID");
                }

                games[request.GameId].PickedDoor = request.Door;

                // Reveal a goat door that is not the picked door or the car door
                int revealedGoatDoor;
                do
                {
                    revealedGoatDoor = random.Next(3);
                } while (revealedGoatDoor == games[request.GameId].CarDoor || revealedGoatDoor == request.Door);

                games[request.GameId].RevealedGoatDoor = revealedGoatDoor;

                _logger.LogInformation("Revealed goat door for GameId: {GameId} is {RevealedGoatDoor}", request.GameId, revealedGoatDoor);

                return Ok(new { RevealedGoatDoor = revealedGoatDoor });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while picking a door for GameId: {GameId}", request.GameId);
                return StatusCode(500, new { Message = "An error occurred while picking a door.", Details = ex.Message });
            }
        }


        [HttpPost("MakeFinalChoice")]
        public IActionResult MakeFinalChoice([FromBody] FinalChoiceRequest request)
        {
            try
            {
                if (!games.ContainsKey(request.GameId))
                {
                    _logger.LogWarning("Invalid Game ID: {GameId}", request.GameId);
                    return BadRequest("Invalid Game ID");
                }

                var game = games[request.GameId]; // Retrieve the game object

                int finalDoor = request.Switch ? 3 - game.PickedDoor - game.RevealedGoatDoor : game.PickedDoor;
                bool win = finalDoor == game.CarDoor;

                games.Remove(request.GameId); // Remove the game from the dictionary

                _logger.LogInformation("GameId: {GameId} - Player {Result}. Car was behind door {CarDoor}.", request.GameId, win ? "won" : "lost", game.CarDoor);

                return Ok(new { Win = win, CarDoor = game.CarDoor }); // Return the result using the retrieved game object
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while making the final choice for GameId: {GameId}", request.GameId);
                return StatusCode(500, new { Message = "An error occurred while making the final choice.", Details = ex.Message });
            }
        }


    }
}

using Microsoft.AspNetCore.Mvc;
using MontyHallApi.Model;

namespace MontyHallApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private static Dictionary<string, GameState> games = new Dictionary<string, GameState>();
        private static Random random = new Random();

        [HttpPost("StartGame")]
        public IActionResult StartGame()
        {
            string gameId = Guid.NewGuid().ToString();
            int carDoor = random.Next(3);
            games[gameId] = new GameState { CarDoor = carDoor, PickedDoor = -1, RevealedGoatDoor = -1 };
            return Ok(new { GameId = gameId });
        }

        [HttpPost("PickDoor")]
        public IActionResult PickDoor([FromBody] PickDoorRequest request)
        {
            if (!games.ContainsKey(request.GameId))
                return BadRequest("Invalid Game ID");

            games[request.GameId].PickedDoor = request.Door;

            // Reveal a goat door that is not the picked door or the car door
            int revealedGoatDoor;
            do
            {
                revealedGoatDoor = random.Next(3);
            } while (revealedGoatDoor == games[request.GameId].CarDoor || revealedGoatDoor == request.Door);

            games[request.GameId].RevealedGoatDoor = revealedGoatDoor;

            return Ok(new { RevealedGoatDoor = revealedGoatDoor });
        }

        [HttpPost("MakeFinalChoice")]
        public IActionResult MakeFinalChoice([FromBody] FinalChoiceRequest request)
        {
            if (!games.ContainsKey(request.GameId))
                return BadRequest("Invalid Game ID");

            var game = games[request.GameId]; // Retrieve the game object

            int finalDoor = request.Switch ? 3 - game.PickedDoor - game.RevealedGoatDoor : game.PickedDoor;
            bool win = finalDoor == game.CarDoor;

            games.Remove(request.GameId); // Remove the game from the dictionary

            return Ok(new { Win = win, CarDoor = game.CarDoor }); // Return the result using the retrieved game object
        }
     
    }
}

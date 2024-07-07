using Microsoft.AspNetCore.Mvc;
using MontyHallApi.Model;

namespace MontyHallApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimulationController : ControllerBase
    {
        private readonly ILogger<SimulationController> _logger;
        public SimulationController(ILogger<SimulationController> logger)
        {
            _logger = logger;
        }

        [HttpGet("simulate")]
        public IActionResult Simulate(int numberOfSimulations, bool changeDoor)
        {
            try
            {
                int wins = 0;
                int losses = 0;
                Random random = new Random();

                for (int i = 0; i < numberOfSimulations; i++)
                {
                    bool result = MontyHallPick(random.Next(3), changeDoor ? 1 : 0, random.Next(3), random.Next(2));
                    if (result)
                        wins++;
                    else
                        losses++;
                }

                _logger.LogInformation("Simulation completed. Number of simulations: {NumberOfSimulations}, Change door: {ChangeDoor}, Wins: {Wins}, Losses: {Losses}", numberOfSimulations, changeDoor, wins, losses);

                return Ok(new { Wins = wins, Losses = losses, Total = numberOfSimulations });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during simulation with {NumberOfSimulations} simulations and ChangeDoor: {ChangeDoor}", numberOfSimulations, changeDoor);
                return StatusCode(500, new { Message = "An error occurred during the simulation.", Details = ex.Message });
            }
        }
        private bool MontyHallPick(int pickedDoor, int changeDoor, int carDoor, int goatDoorToRemove)
        {
            try
            {
                int leftGoat = 0;
                int rightGoat = 2;
                switch (pickedDoor)
                {
                    case 0:
                        leftGoat = 1;
                        rightGoat = 2;
                        break;
                    case 1:
                        leftGoat = 0;
                        rightGoat = 2;
                        break;
                    case 2:
                        leftGoat = 0;
                        rightGoat = 1;
                        break;
                }

                int keepGoat = goatDoorToRemove == 0 ? rightGoat : leftGoat;
                bool result = changeDoor == 0 ? carDoor == pickedDoor : carDoor != keepGoat;

                _logger.LogInformation("MontyHallPick: PickedDoor = {PickedDoor}, ChangeDoor = {ChangeDoor}, CarDoor = {CarDoor}, GoatDoorToRemove = {GoatDoorToRemove}, Result = {Result}",
                    pickedDoor, changeDoor, carDoor, goatDoorToRemove, result);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during MontyHallPick with PickedDoor: {PickedDoor}, ChangeDoor: {ChangeDoor}, CarDoor: {CarDoor}, GoatDoorToRemove: {GoatDoorToRemove}",
                    pickedDoor, changeDoor, carDoor, goatDoorToRemove);
                throw; 
            }
        }
    }
}

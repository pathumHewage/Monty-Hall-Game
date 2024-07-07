using Microsoft.AspNetCore.Mvc;

namespace MontyHallApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimulationController : ControllerBase
    {
        [HttpGet("simulate")]
        public IActionResult Simulate(int numberOfSimulations, bool changeDoor)
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

            return Ok(new { Wins = wins, Losses = losses, Total = numberOfSimulations });
        }

        private bool MontyHallPick(int pickedDoor, int changeDoor, int carDoor, int goatDoorToRemove)
        {
            int leftGoat = 0;
            int rightGoat = 2;
            switch (pickedDoor)
            {
                case 0: leftGoat = 1; rightGoat = 2; break;
                case 1: leftGoat = 0; rightGoat = 2; break;
                case 2: leftGoat = 0; rightGoat = 1; break;
            }

            int keepGoat = goatDoorToRemove == 0 ? rightGoat : leftGoat;
            return changeDoor == 0 ? carDoor == pickedDoor : carDoor != keepGoat;
        }




    }
}

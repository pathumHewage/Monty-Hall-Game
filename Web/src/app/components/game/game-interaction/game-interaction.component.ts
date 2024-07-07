import { Component } from '@angular/core';
import { SimulationService } from 'src/services/simulation.service';

@Component({
  selector: 'app-game-interaction',
  templateUrl: './game-interaction.component.html',
  styleUrls: ['./game-interaction.component.css']
})
export class GameInteractionComponent {
  gameId: string | null = null;
  pickedDoor: number | null = null;
  revealedGoatDoor: number | null = null;
  switch: boolean = false;
  gameResult: string | null = null;
  carDoor: number | null = null;

  constructor(private simulationService: SimulationService) { }

  startGame() {
    this.simulationService.startGame().subscribe(data => {
      this.gameId = data.gameId;
      this.pickedDoor = null;
      this.revealedGoatDoor = null;
      this.gameResult = null;
    });
  }

  pickDoor(door: number) {
    if (!this.gameId) return;

    this.simulationService.pickDoor(this.gameId, door).subscribe(data => {
      this.pickedDoor = door;
      this.revealedGoatDoor = data.revealedGoatDoor;
    });
  }

  makeFinalChoice() {
    if (this.gameId !== null) {
      this.simulationService.makeFinalChoice(this.gameId, this.switch).subscribe(
        (response: any) => {
          console.log(response);
          if (response.win) {
            this.gameResult = 'win';
            this.carDoor = response.carDoor
          } else {
            this.gameResult = 'lost';
          }
        },
        (error) => {
          console.error('Error making final choice:', error);
        }
      );
    }
  }

  resetGame() {
    this.gameId = null;
    this.pickedDoor = null;
    this.revealedGoatDoor = null;
    this.switch = false; // Reset switch door option
    this.gameResult = null;
    this.carDoor = null;
  }
}

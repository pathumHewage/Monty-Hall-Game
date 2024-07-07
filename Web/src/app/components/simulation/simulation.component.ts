import { Component } from '@angular/core';
import { SimulationService } from 'src/services/simulation.service';


@Component({
  selector: 'app-simulation',
  templateUrl: './simulation.component.html',
  styleUrls: ['./simulation.component.css']
})
export class SimulationComponent {
  numberOfSimulations: number = 1000;
  changeDoor: boolean = true;
  results: any = null;

  constructor(private simulationService: SimulationService) { }

  startSimulation() {
    debugger;
    this.simulationService.simulate(this.numberOfSimulations, this.changeDoor)
      .subscribe(data => {
        console.log(data);
        this.results = data;
        console.log(this.results);
      });
  }
}
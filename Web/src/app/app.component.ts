import { Component } from '@angular/core';
import { Router } from '@angular/router';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(private router: Router) {}
  title = 'MontyHallApp';

  playGame() {
    this.router.navigate(['/play-game']); // Navigate to '/play-game' route
  }

  startSimulation() {
    this.router.navigate(['/simulation']); // Navigate to '/simulation' route
  }
}

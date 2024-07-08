import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  constructor(private router: Router) {}
  playGame() {
    this.router.navigate(['/play-game']); // Navigate to '/play-game' route
  }

  startSimulation() {
    this.router.navigate(['/simulation']); // Navigate to '/simulation' route
  }
}

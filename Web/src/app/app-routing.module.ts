import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SimulationComponent } from './components/simulation/simulation.component';
import { GameInteractionComponent } from './components/game-interaction/game-interaction.component';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'simulation', component: SimulationComponent },
  { path: 'play-game', component: GameInteractionComponent }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

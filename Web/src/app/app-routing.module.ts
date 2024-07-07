import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SimulationComponent } from './components/simulation/simulation.component';

const routes: Routes = [
  { path: '', redirectTo: '/simulation', pathMatch: 'full' }, // Default route
  { path: 'simulation', component: SimulationComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

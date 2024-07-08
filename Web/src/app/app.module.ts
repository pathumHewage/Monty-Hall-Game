import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SimulationComponent } from './components/simulation/simulation.component';
import { SimulationService } from 'src/services/simulation.service';
import { GameInteractionComponent } from './components/game-interaction/game-interaction.component';
import { HomeComponent } from './components/home/home.component';


@NgModule({
  declarations: [
    AppComponent,
    SimulationComponent,
    GameInteractionComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [SimulationService],
  bootstrap: [AppComponent]
})
export class AppModule { }

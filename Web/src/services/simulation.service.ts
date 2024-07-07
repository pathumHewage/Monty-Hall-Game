import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SimulationService {

  private apiUrl = 'https://localhost:7202/api/';

  constructor(private http: HttpClient) { }

  simulate(numberOfSimulations: number, changeDoor: boolean): Observable<any> {
    let params = new HttpParams()
      .set('numberOfSimulations', numberOfSimulations.toString())
      .set('changeDoor', changeDoor.toString());

    return this.http.get<any>(`${this.apiUrl}simulation/simulate`, { params });
  }
  startGame(): Observable<any> {
    return this.http.post(`${this.apiUrl}game/StartGame`, {});
  }

  pickDoor(gameId: string, door: number): Observable<any> {
    return this.http.post(`${this.apiUrl}game/PickDoor`, { GameId: gameId, Door: door });
  }

  makeFinalChoice(gameId: string, switchDoor: boolean): Observable<any> {
    return this.http.post(`${this.apiUrl}game/MakeFinalChoice`, { GameId: gameId, Switch: switchDoor });
  }
}

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GameInteractionComponent } from './game-interaction.component';

describe('GameInteractionComponent', () => {
  let component: GameInteractionComponent;
  let fixture: ComponentFixture<GameInteractionComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [GameInteractionComponent]
    });
    fixture = TestBed.createComponent(GameInteractionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BrigadesVolunteerComponent } from './brigades-volunteer.component';

describe('BrigadesVolunteerComponent', () => {
  let component: BrigadesVolunteerComponent;
  let fixture: ComponentFixture<BrigadesVolunteerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BrigadesVolunteerComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BrigadesVolunteerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

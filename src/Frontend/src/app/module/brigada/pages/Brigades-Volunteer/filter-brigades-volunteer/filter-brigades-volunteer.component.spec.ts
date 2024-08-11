import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FilterBrigadesVolunteerComponent } from './filter-brigades-volunteer.component';

describe('FilterBrigadesVolunteerComponent', () => {
  let component: FilterBrigadesVolunteerComponent;
  let fixture: ComponentFixture<FilterBrigadesVolunteerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FilterBrigadesVolunteerComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FilterBrigadesVolunteerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

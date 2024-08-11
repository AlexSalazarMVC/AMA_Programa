import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewVolunterrComponent } from './view-volunterr.component';

describe('ViewVolunterrComponent', () => {
  let component: ViewVolunterrComponent;
  let fixture: ComponentFixture<ViewVolunterrComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ViewVolunterrComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewVolunterrComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

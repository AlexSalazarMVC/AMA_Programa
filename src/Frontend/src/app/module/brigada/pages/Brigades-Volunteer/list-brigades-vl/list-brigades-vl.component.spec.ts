import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListBrigadesVlComponent } from './list-brigades-vl.component';

describe('ListBrigadesVlComponent', () => {
  let component: ListBrigadesVlComponent;
  let fixture: ComponentFixture<ListBrigadesVlComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListBrigadesVlComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListBrigadesVlComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

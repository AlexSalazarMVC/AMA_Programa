import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateBrigVolunterComponent } from './create-brig-volunter.component';

describe('CreateBrigVolunterComponent', () => {
  let component: CreateBrigVolunterComponent;
  let fixture: ComponentFixture<CreateBrigVolunterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateBrigVolunterComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateBrigVolunterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import {
  Component,
  AfterViewInit,
  OnInit,
  EventEmitter,
  Output,
  ChangeDetectorRef,
  OnChanges,
  SimpleChanges,
  OnDestroy,
} from '@angular/core';
import { FormGroup, FormControl, FormBuilder,Validators } from '@angular/forms';
import { volunteer } from '../../../../../models/Volunteer/Volunteer';  
import { BrigadeVlServices } from '../Services/BrigadeVlServices'; 
import { FormService } from '../../../../../shared/services/from.service'; 
import { BrigadeForm } from '../../../interfaces/brigade-form'; 
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { ConfSystemServiceService } from '../../../../../data/conf-system-service.service'; 
import { ReactiveFormsModule } from '@angular/forms';
@Component({
  selector: 'app-view-volunterr',
  templateUrl: './view-volunterr.component.html',
  standalone: true,
  imports: [
  
    ReactiveFormsModule
   
   
  ],
  styleUrl: './view-volunterr.component.sass'
})
export class ViewVolunterrComponent implements OnInit{


 brigadeForm!: FormGroup;
 update: boolean = false;
 view: boolean = false;
 label: string = 'Crear';
  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private fb: FormBuilder,
    private cdr: ChangeDetectorRef
  ) {
   
  }
 
  InitializerData() {
  
  }

  ngOnInit(): void {
    this.update = this.config.data.update;
    this.view = this.config.data.view;
   
    this.brigadeForm = this.fb.group({
      id: [{ value: '', disabled: true }],
      brigadeid: [{ value: '', disabled: this.view }, [Validators.required, Validators.minLength(3), Validators.maxLength(10)]],
      idvoluntario:[{ value: '', disabled: this.view }, Validators.required],
      nombrebrig: [{ value: '', disabled: this.view }, Validators.required],
      responbrigada: [{ value: '', disabled: this.view }, Validators.required],
      nombrevolun: [{ value: '', disabled: this.view }, Validators.required],
      descripcion: [{ value: '', disabled: this.view }, Validators.required]
    });

    if (this.update) {
      this.label = 'Actualizar';
      this.populateForm(this.config.data.brigade);
    }

    this.cdr.detectChanges();

  }
 
 
  private populateForm(brigade: volunteer) {
    this.brigadeForm.patchValue({
      id: brigade.id,
      brigadeid: brigade.brigadeId,
      idvoluntario: brigade.volunteerId,
      nombrebrig: brigade.nombredeBrigada,
      responbrigada: brigade.responsableBrigada,
      nombrevolun:brigade.nombreVoluntario,
      descripcion: brigade.descripcionBrigada,
      est:brigade.status,
      
      

      
    });
  }

}

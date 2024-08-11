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
import { FormGroup, FormControl, Validators,FormBuilder } from '@angular/forms';
import {ChangeItemDropdown,
  ConfigurationDropdownProp,
  DynamicDataToDialog, ItemDropdown, } from '../../../../../core/interfaces/ItemDropdown.models'; 
import { FormService } from '../../../../../shared/services/from.service'; 
import { BrigadeForm } from '../../../interfaces/brigade-form';
import { DialogService, DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { BrigadeService } from '../../../services/brigade.service'; 
import { ConfSystemServiceService } from '../../../../../data/conf-system-service.service'; 
import { ReactiveFormsModule } from '@angular/forms';
import { PersonDto } from '../../../../person/interfaces/person-dto';
import { PersonFilter } from '../../../../person/interfaces/person-filter'; 
import { PersonService } from '../../../../person/services/person.service';
import { BrigadeFilter } from '../../../models/brigada-filter.interface';
import { BrigadeDto } from '../../../interfaces/brigade-dto';
import { BrigadeVlServices } from '../Services/BrigadeVlServices';
import { Brigadevolunteer } from '../../../../../models/Volunteer/BrigadeVolunteer';
import { MessageService } from 'primeng/api';
import { BrigadeDialogComponent } from '../../../../../shared/component/brigade-dialog/brigade-dialog.component';
import { volunteer } from '../../../../../models/Volunteer/Volunteer';
import { PersonDialogComponet } from '../../../../../shared/component/person-dialog-componet/person-dialog-componet.component';
import { HttpErrorResponse } from '@angular/common/http';
@Component({
  selector: 'app-create-brig-volunter',
  templateUrl: './create-brig-volunter.component.html',
  styleUrl: './create-brig-volunter.component.sass'
})
export class CreateBrigVolunterComponent implements OnDestroy, OnInit, OnChanges{
  personaConfig!: ConfigurationDropdownProp;
  BrigadaConfig!: ConfigurationDropdownProp;
  lsPersona!: DynamicDataToDialog;
  lsBrigadas!: DynamicDataToDialog;
  itemsPersona: ItemDropdown[] = [];
  itemsBrigadas: ItemDropdown[] = [];
  @Output() isUpdateListDetails = new EventEmitter<boolean>();

  brigadeForm!: FormGroup;
  formData!: BrigadeForm;
  update: boolean = false;
  label: any = 'Crear';
  Personas: PersonDto | null = null;
  Brigadasvolun: volunteer | null = null;
  Personastado: PersonDto[] = [];
  loading: boolean = false;
  view: boolean = false;
  personFiler!:PersonFilter;
  filteredPersonas: PersonDto[] = [];
  hasActiveVolunteer: boolean = false;
  brigadeFiler!: BrigadeFilter;
  listaBrigadeas: BrigadeDto[] = [];
  creabrigada: Brigadevolunteer[] = [];
  volunteer: Volunteer[]=[]
  totalRows: number = 0;
 
  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private formService: FormService,
    private brigadeService: BrigadeService,
    private configService: ConfSystemServiceService,
    private personService: PersonService,
    private BrigadevlService: BrigadeVlServices,
    private cdr: ChangeDetectorRef,
    private messageService: MessageService,
    private dialogService: DialogService,
    private fb :FormBuilder
  ) {
    this.InitializeData();
  }
  ngOnInit(): void {
    this.InitializerData();
 
  }
  ngOnDestroy(): void {
 
    if (this.ref) {
      this.ref.close();
     
    }

  }

 

getPersonasid() {
  this.personFiler= {
    offset: 0,
    take: 10,
    sort: '',
  }
  this.loading = true;
 
  const id = this.brigadeForm.get('personId')?.value;
 
  this.personService.getPersonById(id).subscribe(
    (result) => {
      this.Personas = result.result; 
      this.itemsPersona = [{
        code: this.Personas.id.toString(),
        description: `${this.Personas.nameCompleted} `,
      }];
      this.loading = false;
     
    },
    (error) => {
      console.error('Error fetching personas', error);
      this.loading = false;
    }
  );
}

  
  private buildFormData() {

    this.formData = {
      ...this.config.data.brigade,
      start: new Date(this.config.data.brigade.start),
      end: new Date(this.config.data.brigade.end),
      id: this.config.data.brigade.brigadeId,
      idbrig: this.config.data.brigade.id, 
      personId: this.config.data.brigade.volunteerId,
    };
   


    this.brigadeForm = this.formService.createFormGroup<BrigadeForm>(this.formData);
    this.validator();
    if (this.update  || this.view ) {
      this.brigadeForm.addControl('idbrig', new FormControl('', [Validators.required]));
    }
  
  
  }
 

  private validator() {
    this.brigadeForm.get('name')?.setValidators([Validators.required]);
    this.brigadeForm.get('description')?.setValidators([Validators.required]);
    this.brigadeForm.get('end')?.setValidators([Validators.required]);
    this.brigadeForm.get('start')?.setValidators([Validators.required]);

    this.brigadeForm
      .get('personId')
      ?.setValidators([Validators.required, Validators.min(0)]);
      this.brigadeForm
      .get('id')
      ?.setValidators([Validators.required, Validators.min(0)]);
      this.brigadeForm
      .get('idbrig')
      ?.setValidators([Validators.required]);
      
  }

  
  ngOnChanges(changes: SimpleChanges): void {
  
  }

  onItemChanged(eventData: ChangeItemDropdown) {
    if (eventData && eventData.conf.Id === 'PersonId') {
      this.brigadeForm.get('personId')?.setValue(eventData.data.code);
      console.log(eventData.data.code+"codigo");
     
    }
    if (eventData && eventData.conf.Id === 'id') {
      this.brigadeForm.get('id')?.setValue(eventData.data.code);
      console.log(eventData.data.code+"codigo");

    }
    if (eventData && eventData.conf.Id === 'idbrig') {
      this.brigadeForm.get('idbrig')?.setValue(eventData.data.code);
      console.log(eventData.data.code+"codigo");
    }
    //this.ref.close();
  }
  InitializeData() {
 
   
    this.update = this.config.data.update;
    this.view = this.config.data.view;

    if (this.update) {
     
      this.label = 'Actualizar';
      this.buildFormData();
      this.populateDropdowns();
      this.getBrigadeid(); 
    } else {
      this.label = 'Crear';
      this.buildForm();
    }
   
    this.validator();
  }
  
  async Updatebrigade() {
    try {
      await this.BrigadevlService
        .updateBrigade(this.brigadeForm.value)
        .toPromise();
        this.messageService.add({
          severity: 'success',
          summary: 'Exito',
          detail: 'Se han actualizado los datos',
          life: 3000,
        });

      // The HTTP request has completed
    } catch (error) {
      console.log(error);
      this.messageService.add({
        severity: 'warn',
        summary: 'Error',
        detail: 'No se permiten duplicados',
        life: 3000,
      });



      // Handle the error
    }
  }

  buildForm() {
    this.formData = {
   
description: '',
      end: undefined,
      name: '',
      start: new Date(),
      companyId: 0,
      personId: 0,
      id:0,
   
    };
    this.brigadeForm = this.formService.createFormGroup<BrigadeForm>(
      this.formData
    );
  }
  populateDropdowns() {
    this.lsPersona = { Params: [] };
    this.personaConfig = {
      Id: 'PersonId',
      Name: 'Persona',
      Tooltip: 'Buscar Persona',
      Dataset: 'Persona',
      NameComponent: 'PersonDialogComponet',
    };
  
    this.lsBrigadas = { Params: [] };
    this.BrigadaConfig = {
      Id: 'id',
      Name: 'BrigadasVoluntario',
      Tooltip: 'Buscar Brigada',
      Dataset: 'Brigade',
      NameComponent: 'BrigadeDialogComponent',
    };

   
  }



  InitializerData() {
  
   

   
    this.Volunteers();
    this.lsPersona = { Params: [] };
    this.personaConfig = {
      Id: 'PersonId',
      Name: 'Persona',
      Tooltip: 'Search Persona',
      Dataset: 'Persona',
      NameComponent: 'PersonDialogComponet',
      
    };
  
    this.itemsPersona = [];
    if (this.update || this.view) {

      this.itemsPersona.push({
        code: this.brigadeForm.get('personId')?.value,
        description:
          String(this.brigadeForm.get('identificationPerson')?.value) +
          '-' +
          String(this.brigadeForm.get('nameCompletedPerson')?.value),
      });
    }
  }
  private getBrigadeid() {
    this.brigadeFiler = {
      offset: 0,
      take: 10,
      sort: '',
    };
    const id = this.brigadeForm.get('idbrig')?.value;
    this.BrigadevlService.getBrigadeById(id).subscribe(
      (result) => {
        this.Brigadasvolun = result.result;
        
       this.itemsBrigadas = [{
        code: this.Brigadasvolun.brigadeId.toString(),
        description: `${this.Brigadasvolun.nombredeBrigada} `,
      }];
      this.itemsPersona = [{
        code: this.Brigadasvolun.brigadeId.toString(),
        description: `${this.Brigadasvolun.nombreVoluntario} `,
      }];
    
      this.loading = false;
      console.log(JSON.stringify( this.Brigadasvolun)+ "idd"+id);
    },
    (error) => {
      console.error('Error fetching personas', error);
      this.loading = false;
    }
  );
}


  updateBrigadeMethod(brigade: Brigadevolunteer) {
    
    this.BrigadevlService.updateBrigade(brigade).subscribe(
      result => {
        this.messageService.add({ severity: 'success', summary: 'Exito', detail: 'La brigada se actulizo correctamente' });
        this.ref.close(result);
      },
      
    );
      
    
  }
  async Createbrigade() {
    if (this.brigadeForm.get('idbrig')?.invalid || this.brigadeForm.get('personId')?.invalid) {
      this.messageService.add({
        severity: 'warn',
        summary: 'Error',
        detail: 'Por favor complete los campos requeridos.',
        life: 3000,
      });
      return;
    }
    const brigades: Brigadevolunteer = {
      id: this.brigadeForm.get('idbrig')?.value,
      brigadeId: this.brigadeForm.get('id')?.value,
      volunteerId: this.brigadeForm.get('personId')?.value,
      status: 'A'
    };
  
    console.log(brigades);
  
    try {
      if (this.update) {
        const result = await this.updateBrigadeMethod(brigades);
       
      } 
    } catch (error) {
    
    }
  }
  
  handleSuccessResponse(result: any) {
   
    if (result) {
     console.log(result);
      setTimeout(() => {
        this.ref.close(this.brigadeForm.value);
        this.isUpdateListDetails.emit(true);
      }, 1000);
    } 
  }

  Volunteers(){
   
    this.lsBrigadas = { Params: [] };
    this.BrigadaConfig = {
      Id: 'id',
      Name: 'BrigadasVoluntario',
      Tooltip: 'Search Brigada',
      Dataset: 'Brigade',
      NameComponent: 'BrigadeDialogComponent',
    };
    this.itemsBrigadas = [];
    if (this.update || this.view) {
      this.itemsBrigadas.push({
        code: this.brigadeForm.get('id')?.value,
        description:
          String(this.brigadeForm.get('Name')?.value)
      });
    }

  }
}

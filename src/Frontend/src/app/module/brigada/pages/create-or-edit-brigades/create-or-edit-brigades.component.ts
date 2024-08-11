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
  ViewChild,
} from '@angular/core';
import { FormGroup, FormControl, Validators,FormBuilder } from '@angular/forms';
import {
  ChangeItemDropdown,
  ConfigurationDropdownProp,
  DynamicDataToDialog,
  ItemDropdown,
} from '../../../../core/interfaces/ItemDropdown.models';
import { FormService } from '../../../../shared/services/from.service';
import { BrigadeForm } from '../../interfaces/brigade-form';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { BrigadeService } from '../../services/brigade.service';
import { ConfSystemServiceService } from '../../../../data/conf-system-service.service';
import { MessageService } from 'primeng/api';
import { DropdownComponent } from '../../../../shared/component/dropdown-component/dropdown-component.component';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-create-or-edit-brigades',
  templateUrl: './create-or-edit-brigades.component.html',
  styleUrl: './create-or-edit-brigades.component.sass',
})
export class CreateOrEditBrigadesComponent
  implements OnDestroy, OnInit, OnChanges
{
  personaConfig!: ConfigurationDropdownProp;
  lsPersona!: DynamicDataToDialog;
  @ViewChild(DropdownComponent) dropdownComponent!: DropdownComponent;
  itemsPersona: ItemDropdown[] = [];
  @Output() isUpdateListDetails = new EventEmitter<Boolean>();

  brigadeForm!: FormGroup;
  formData!: BrigadeForm;
  update: boolean = false;
  label: any = 'Crear';
 
  loading: boolean = false;
  view: boolean = false;
  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private formService: FormService,
    private brigadeService: BrigadeService,
    private configService: ConfSystemServiceService,
    private messageService: MessageService,
    private cdr: ChangeDetectorRef,
    private fb: FormBuilder
  ) {



   // this.InitializeData();
  }
  ngOnDestroy(): void {
    if (this.ref) {
      this.ref.close();
    }
  }
  ngOnChanges(changes: SimpleChanges): void {}

  onItemChanged(eventData: ChangeItemDropdown) {
    if (eventData && eventData.conf.Id === 'idPersona') {
      this.brigadeForm.get('personId')?.setValue(eventData.data.code);
    }
  }
  private buildFormData() {
    this.formData = {
      ...this.config.data.brigade,
      start: new Date(this.config.data.brigade.start),
      end: new Date(this.config.data.brigade.end),
    };

    this.brigadeForm = this.formService.createFormGroup<BrigadeForm>(
      this.formData
    );
    this.validator();
  }
  InitializerData() {
    this.lsPersona = { Params: [] };
    this.personaConfig = {
      Id: 'idPersona',
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

  ngOnInit(): void {
    this.InitializeData();
    console.log("oninit create",this.isUpdateListDetails);
  }
  InitializeData() {
    this.update = this.config.data.update;
    this.view = this.config.data.view;

    if (this.update) {
      this.label = 'Actualizar';
      this.buildFormData();
    } else {
      this.label = 'Crear';
      this.buildForm();
    }
    this.InitializerData();
    this.validator();
  }

  InputData() {
    if (this.update) {
      this.Updatebrigade();
      setTimeout(() => {
        
        this.ref.close(this.brigadeForm.value);
        this.isUpdateListDetails.emit(true);
  console.log("inputdataupdate",this.isUpdateListDetails);

      }, 1000);
     
    } else {
      if (this.brigadeForm.invalid || this.dropdownComponent.formItemDropdownGroup.invalid) {
        return;
      }
    
      this.Createbrigade();
      setTimeout(() => {
       
        this.ref.close(this.brigadeForm.value);
        this.isUpdateListDetails.emit(true);
        console.log("inputdatacreate",this.isUpdateListDetails);

      }, 1000);
     // this.isUpdateListDetails.emit(false);
    }
  }

  async Createbrigade() {
    try {
      await this.brigadeService
        .createBrigade(this.brigadeForm.value)
        .toPromise();
        console.log("createbolunbefore",this.isUpdateListDetails);
        this.isUpdateListDetails.emit(true);
        console.log("createbolun",this.isUpdateListDetails);
    } catch (error) {


    }
 
  }

  async Updatebrigade() {
    try {
      await this.brigadeService
        .updateBrigade(this.brigadeForm.value)
        .toPromise();
        this.messageService.add({
          severity: 'success',
          summary: 'Exito',
          detail: 'Se han actualizado los datos',
          life: 3000,
        });
      this.isUpdateListDetails.emit(true);
      console.log("update",this.isUpdateListDetails);
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
  closeDialog() {
    this.ref.close();
   // this.isUpdateListDetails.emit(true);
  }

  onIdentificationTypeChange(value: any) {
    this.brigadeForm.patchValue({
      identificationTypeId: value, // Actualiza el valor de identificationTypeId en el formulario
    });
  }

  buildForm() {
    this.formData = {
      description: '',
      end: undefined,
      name: '',
      start: new Date(),
      companyId: 0,
      personId: 0,
    
    };
    this.brigadeForm = this.formService.createFormGroup<BrigadeForm>(
      this.formData
    );
  }

  private validator() {
    this.brigadeForm.get('name')?.setValidators([Validators.required]);
    this.brigadeForm.get('description')?.setValidators([Validators.required]);
    this.brigadeForm.get('end')?.setValidators([Validators.required]);
    this.brigadeForm.get('start')?.setValidators([Validators.required]);

    this.brigadeForm
      .get('personId')
      ?.setValidators([Validators.required]);
  }

  valorDate(field: string) {
    const fieldValue = this.brigadeForm.get(field)?.value;
    if (fieldValue instanceof Date) {
      // Formatear la fecha utilizando toLocaleDateString
      return fieldValue.toLocaleDateString('en-US', {
        month: '2-digit',
        day: '2-digit',
        year: 'numeric',
      });
    }

    // Si no es una fecha, devolver el valor sin formatear
    return fieldValue || '';
  }
}

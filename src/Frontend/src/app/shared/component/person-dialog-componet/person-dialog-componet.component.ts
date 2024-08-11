import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import {
  ChangeItemDropdown,
  ConfigurationDropdownProp,
  DynamicDataToDialog, ItemDropdown
} from '../../../core/interfaces/ItemDropdown.models';
import { ResultList } from '../../../core/interfaces/result';
import { Sort } from '../../../core/interfaces/sort';
import { TextValidatorService } from '../../../core/validators/text-validator.service';
import { BrigadeDto } from '../../../module/brigada/interfaces/brigade-dto';
import { BrigadeVlServices } from '../../../module/brigada/pages/Brigades-Volunteer/Services/BrigadeVlServices';
import { PersonDto } from '../../../module/person/interfaces/person-dto';
import { PersonFilter } from '../../../module/person/interfaces/person-filter';
import { PersonaDialogService } from './persona-dialog.service';
@Component({
  selector: 'app-person-dialog-componet',

  templateUrl: './person-dialog-componet.component.html',
  styleUrl: './person-dialog.css'
})
export class PersonDialogComponet implements OnInit {
  irAPaginaCrearPersona() {

    this.router.navigate(['configuracion/person']);

    this.ref.close();
  }
  lsPersonas!: DynamicDataToDialog;
  personaConfig!: ConfigurationDropdownProp;
  BrigadaConfig!: ConfigurationDropdownProp;
  itemsPersona: ItemDropdown[] = [];
  lsBrigadas!: DynamicDataToDialog;
  brigadeForm!: FormGroup;
  listaBrigadeas: BrigadeDto[] = [];
  itemsBrigadas: ItemDropdown[] = [];
  isFirstLoad: boolean = true;
  loading: boolean = false;
  totalRows: number = 0;
  lsPersona: PersonDto[] = [];
  codigosMessage: any;
  mensajesMessage: any;
  selectionPerson?: PersonDto;
filterVolunteers: boolean = false; 
Dropdown: boolean = false;
view: boolean = false;  
selectionPersona: number[] = [];
showDropdown: boolean = false; 
@Input() ShowDropdown: boolean = false;
 disabledPropControl: boolean = false;
 conflictIds: { brigadeId: number, volunteerId: number }[] = [];
 conflictId: {  volunteerId: number }[] = [];
personas: PersonDto[] = [
  
  // Aquí deberías tener los datos de tus personas
];

allSelected: boolean = false;
  constructor(private personaDialogService: PersonaDialogService
    , public ref: DynamicDialogRef
    , public config: DynamicDialogConfig
    , private router: Router
    , private messageService: MessageService
    , private readonly textValidatorService: TextValidatorService
    ,private brigser :BrigadeVlServices) {
  }

  ngOnInit(): void {
    this.Dropdown = this.config.data.dropdown; // o asignar según tu lógica
    this.view = this.config.data.view; // o asignar según tu lógica

    this.filterVolunteers = this.config.data.filterVolunteers;
    let personaReq: PersonFilter = {
      ...(this.config.data.dataFilter ? this.config.data.dataFilter : {}),
      offset: 0,
      take: 10
    };
    this.Volunteers();
    this.getPersonaData(personaReq);
    
   
  }
  
 
  onItemChanged(eventData: ChangeItemDropdown) {
   
    if (eventData && eventData.conf.Id === 'id') {
      (document.getElementById('id') as HTMLInputElement).value = eventData.data.code;
      console.log(eventData.data.code + "codigo");
    }
  
  }
  loadPersonaLazy(event: any) {
    if (this.isFirstLoad) {
      this.isFirstLoad = false;
      return;
    }

    let personaReq: PersonFilter = {
      ...(this.config.data.dataFilter ? this.config.data.dataFilter : {}),
      offset: event.first,
      take: event.rows,
      sort: ""
    };

    let sortCol = event.sortField;
    let sortColOrder = event.sortOrder;
    let sortStr = "";
    if (!(sortCol === undefined || sortCol === null)) {
      let sortArray: Sort[] = [];
      let sortObj: Sort = {
        selector: sortCol,
        desc: sortColOrder !== 1,
      };
      sortArray.push(sortObj);
      sortStr = JSON.stringify(sortArray);
    }
    personaReq.sort = sortStr;

    let filterObj = event.filters;
    if (filterObj.hasOwnProperty('DocumentoIdentidad')) {
      personaReq.identification = filterObj['DocumentoIdentidad']['value'];
    }
    if (filterObj.hasOwnProperty('Nombre')) {
      personaReq.firstName = filterObj['Nombre']['value'];
    }
    if (filterObj.hasOwnProperty('Apellido')) {
      personaReq.lastName = filterObj['Apellido']['value'];
    }

    this.getPersonaData(personaReq);
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
   
  }
  getPersonaData(request: PersonFilter) {
    [this.codigosMessage, this.mensajesMessage]
    this.loading = true;
    this.personaDialogService.getPaginated(request)
      .subscribe(
        {
          next: (data) => {
            let dataTmp = <ResultList<PersonDto>>data;
            if (dataTmp) {
              this.lsPersona = dataTmp.result;
              this.totalRows = dataTmp.length;
              
              
             // this.lsPersona = dataTmp.result.filter(persona => persona.volunteer === true);
             if (this.filterVolunteers) {
              this.lsPersona = dataTmp.result.filter(persona => persona.volunteer === true);
              this.personas = dataTmp.result.filter(persona => persona.volunteer === true);
             
            } else{
            this.lsPersona = dataTmp.result;
          //    this.lsPersona = this.personas;
            }
     
           this.allSelected = this.selectionPersona.length === this.personas.length;
            }
          },
          error: (error) => {
            this.loading = false;
            if (error) {

            }
          },
          complete: () => {
            this.loading = false;
          }
        }
      );
  }

  onRowDblClick(event: any, dataSelected: any) {


    if (dataSelected) {
      let _description = ((dataSelected.identification) ? dataSelected.identification : "") + " - " +
        ((dataSelected.firstName) ? dataSelected.firstName : "") + " - " +
        ((dataSelected.lastName) ? dataSelected.lastName : "");
      let itemDropdown: ItemDropdown = {
        code: dataSelected.id,
        description: _description,
        dataSerialize: JSON.stringify(dataSelected)
      }
      this.ref.close(itemDropdown);
    }

  }
  onRowClick(dataPersona: any) {
   

      const isChecked = this.isChecked(dataPersona);
      if (isChecked) {
        this.selectionPersona = this.selectionPersona.filter(id => id !== dataPersona.id);
      } else {
        this.selectionPersona.push(dataPersona.id);
      }
    
  
  }
  onAllCheckboxChange(event: Event) {
   
    const checkbox = event.target as HTMLInputElement;
    this.allSelected = checkbox.checked;
   
    if (this.allSelected) {
      this.selectionPersona =  this.personas.map(person => person.id)
 
     
    } else {
      this.selectionPersona = []; // Desmarca todos los ids
    }
   
  }
  isChecked(person: PersonDto): boolean {
    return this.selectionPersona.includes(person.id);
  }
  onCheckboxChange(person: PersonDto, event: Event) {
    const checkbox = event.target as HTMLInputElement;
    if (checkbox.checked) {
      this.selectionPersona.push(person.id); // Solo almacena el id
    } else {
      this. selectionPersona = this.selectionPersona.filter(id=> id !== person.id); 
    }
   
  }
  saveSelectedPersons() {
 
    const brigadeId = (document.getElementById('id') as HTMLInputElement).value; 
    const status = 'A'; // Cambia esto según el contexto
  
    const payload = this.selectionPersona.map(id => ({
      brigadeId,
      volunteerId: id,
      status
    }));
 
    this.brigser.createBrigadeVolunteerLista(payload).subscribe(response => {
      console.log('Response:', response); // Manejar la respuesta
      console.log('codigo:', response.statusCode);
      this.messageService.add({
        severity: 'success',
        summary: 'Éxito',
        detail: 'Brigadas Creadas con éxito',
        life: 3000,
      });
     this.ref.close();
   
    }, error => {
      if (error.status === 409) {
        this.conflictIds = this.extractConflictIds(error.error);
       // const conflictIds =  this.extractConflictIds( error.error);
       const conflictIdsStr = this.conflictIds.map(id => `BrigadeId: ${id.brigadeId}, VolunteerId: ${id.volunteerId}`).join('; ');
        
        this.messageService.add({
          severity: 'warn',
          summary: 'Error',
          detail: `Uno o más registros ya existen en esta brigada: ${conflictIdsStr}`,
          life: 3000,
        });
     console.log(`Uno o más registros ya existen: ${conflictIdsStr}`);
      }
      if (error.status === 400 || error.status === 500) {
        this.messageService.add({
          severity: 'warn',
          summary: 'Error',
          detail: 'Las solicitudes no pueden estar vacías',
          life: 3000,
        });
      }
      console.error('Error:', error); // Manejar el error
    });
  

}


private extractConflictIds(errorMessage: string): { brigadeId: number, volunteerId: number }[] {
  const regex = /BrigadeId (\d+) y VolunteerId (\d+)/g;
  let match;
  const ids = [];

  while ((match = regex.exec(errorMessage)) !== null) {
    
    ids.push({ brigadeId: parseInt(match[1]), volunteerId: parseInt(match[2]) });
  }

  return ids;
}



  onKeyDownLetters = (event: any) => this.textValidatorService.validateOnlyLetters(event);
  onKeyDownLettersAndNumbers = (event: any) => this.textValidatorService.validateOnlyLettersAndNumbers(event);
  onInput = (event: any) => this.textValidatorService.changeUppercase(event);
}

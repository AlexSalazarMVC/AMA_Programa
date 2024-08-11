import {
  Component,
  EventEmitter,
  Input,
  OnChanges,
  OnDestroy,
  OnInit,
  Output,
  SimpleChanges,
} from '@angular/core';

import { DialogService } from 'primeng/dynamicdialog';
import { BrigadeFilter } from '../../../models/brigada-filter.interface'; 
import { volunteer } from '../../../../../models/Volunteer/Volunteer'; 
import { BrigadeVlServices } from '../Services/BrigadeVlServices'; 
import { ConfirmationService, MessageService } from 'primeng/api';
import { Sort } from '../../../../../core/interfaces/sort'; 
import { AppModule } from '../../../../../app.module'; 
import { PersonFilter } from '../../../../person/interfaces/person-filter'; 

import { ViewVolunterrComponent } from '../view-volunterr/view-volunterr.component'; 
import { PersonDto } from '../../../../person/interfaces/person-dto'; 
import { PersonService } from '../../../../person/services/person.service'; 

import { CreateOrEditBrigadesComponent } from '../../create-or-edit-brigades/create-or-edit-brigades.component'; 
import { CreateBrigVolunterComponent } from '../create-brig-volunter/create-brig-volunter.component';
import { BrigadeDto } from '../../../interfaces/brigade-dto'; 
import { Brigadevolunteer } from '../../../../../models/Volunteer/BrigadeVolunteer'; 
import { BrigadeVolunteerFilter } from '../../../models/brigadaVolunteer-filter';


@Component({
  selector: 'app-list-brigades-vl',
  templateUrl: './list-brigades-vl.component.html',
  styleUrl: './list-brigades-vl.component.sass'
})
export class ListBrigadesVlComponent {


  @Input() queryRequest: BrigadeVolunteerFilter | undefined;
  @Input() isUpdateListDetails: boolean = true;
 
  @Input() searchFilter: any = {};
  loading: boolean = false;
  listaVoluntario: volunteer[] = [];
  Personastado: PersonDto[] = [];
  totalRows: number = 0;
  brigadeFiler!: BrigadeVolunteerFilter;
  personFiler!:PersonFilter;
  filteredPersonas: PersonDto[] = [];
  hasActiveVolunteer: boolean = false;
  openFilterPanel = true;
  entityFilterRequest: BrigadeVolunteerFilter | undefined;
 

 

  cambios: any;

  storedFilter: BrigadeFilter | undefined;
  constructor(
    private dialogService: DialogService,
    private BridagevlService: BrigadeVlServices,
    private messageService: MessageService,
    private confirmationService: ConfirmationService,
    private personService: PersonService,
   
  ) {}

  private handleUpdateListDetails() {
    this.getBrigade(); // Llamada a la función que obtiene las brigadas
  }
  ngOnChanges(changes: SimpleChanges): void {
    for (let change in changes) {
      if (change === 'isUpdateListDetails') {
        if (this.isUpdateListDetails) {
          this.handleUpdateListDetails();
        }
      }
      if (change === 'searchFilter') {
        if (changes[change].currentValue) {
          // console.log(changes[change].currentValue)
          this.listaVoluntario = changes[change].currentValue.listaVoluntario;
          this.totalRows = changes[change].currentValue.totalRows;
          this.loading = changes[change].currentValue.loading;
        }
      }
    }
    if (this.searchFilter && this.searchFilter.listabeneficiarios) {
      this.listaVoluntario = this.searchFilter.listabeneficiarios;
    }
    
  }
  DeleteData(brigade: Brigadevolunteer, event: Event) {
    this.confirmationService.confirm({
      target: event.target as EventTarget,
      message: '¿Estás seguro de realizar este proceso?',
      header: 'Confirmación',
      icon: 'pi pi-info-circle',
      acceptButtonStyleClass: 'p-button-danger p-button-text',
      rejectButtonStyleClass: 'p-button-text p-button-text',
      acceptIcon: 'none',
      rejectIcon: 'none',
      acceptLabel: 'Confirmar',
      rejectLabel: 'Cancelar',
      accept: () => {
       brigade.status = 'E'
        this.totalRows -= 1;
       this.deleteBrigade(brigade.id);
       
      },
      reject: () => {
        this.messageService.add({
          severity: 'warn',
          summary: '',
          detail: 'Registro no eliminado',
          life: 3000,
        });
      },
    });
  }
  openCloseFilter() {
    this.openFilterPanel = !this.openFilterPanel;
  }

 


  private deleteBrigade(id: number) {
    this.BridagevlService.deleteBrigade(id).subscribe({
      
      
      error: () => {},
    });
  }


  filterPersonas() {
    // Filtrar solo las personas que tienen el valor `volunteer` que deseas
    this.filteredPersonas = this.Personastado.filter(persona => persona.volunteer === true);
}
getPersonas() {
  this.loading = true;
  this.personService.getAllPersons(this.personFiler).subscribe(
      (result) => {
          this.Personastado = result.result;
          this.hasActiveVolunteer = this.Personastado.some(persona => persona.volunteer === true);
          this.filterPersonas(); // Llamar al método para filtrar personas
          this.loading = false;
      },
      (error) => {
          console.error('Error fetching personas', error);
          this.loading = false;
      }
  );
}

  ngOnInit(): void {
   
    this.getBrigade();
  if (this.searchFilter && this.searchFilter.listabeneficiarios) {
      this.listaVoluntario = this.searchFilter.listabeneficiarios;
    }


  }


 
  EditData(brigade: Brigadevolunteer) {
    this.NavigateUpdate(brigade);
  }
  
  NavigateUpdate(brigade:  Brigadevolunteer) {
    this.dialogService
    .open(CreateBrigVolunterComponent, {
      header: 'Actualizar Brigada',
      width: 'auto',
      height: 'auto',
      data: { 
        update: true, 
        brigade: brigade 
      },
      contentStyle: { 'min-height': '150px', 'min-width': '200px' },
      baseZIndex: 10000,
    })
    .onClose.subscribe((result) => {
      if (result) {
        this.getBrigade();
      }
    });
 
  }

  ViewData(brigade: BrigadeVlServices) {
    this.NavigateView(brigade);
  }
  loadDetailsLazy(event: any) {
    let sortCol = event.sortField;
    let sortColOrder = event.sortOrder;
    let offset = event.first;
    let take = event.rows;
    let sortStr = '';
    if (!(sortCol === undefined || sortCol === null)) {
      let sortArray: Sort[] = [];
      let sortObj: Sort = {
        selector: sortCol,
        desc: sortColOrder !== 1,
      };
      sortArray.push(sortObj);
      sortStr = JSON.stringify(sortArray);
    }

    this.brigadeFiler.sort = sortStr;
    this.brigadeFiler.take = take;
    this.brigadeFiler.offset = offset;
   
    this.BridagevlService.getAllBrigades(this.brigadeFiler).subscribe(
      (result) => {
        this.listaVoluntario = result.result;
   
        this.totalRows = result.length;
        this.loading = false;
      },
      (error) => {}
    );
  }
  NavigateView(brigade: any) {
    this.dialogService.open(ViewVolunterrComponent, {
      header: 'Información de la Brigada',
      width: 'auto',
      height: 'auto',
      data: { update: true, brigade: brigade, view: true },
      contentStyle: { 'min-height': '300px', 'min-width': '200px' },
      baseZIndex: 10000,
    });
  }
  private getBrigade() {
    this.brigadeFiler = {
      offset: 0,
      take: 10,
      sort: '',
    };

    this.BridagevlService.getAllBrigades(this.brigadeFiler).subscribe(
      (result) => {
        this.listaVoluntario = result.result;
 console.log(JSON.stringify(this.listaVoluntario, null)); // Convertir a JSON string
        this.totalRows = result.length;
        this.loading = false;
      },
      (error) => {}
    );
  }
  }

 
  


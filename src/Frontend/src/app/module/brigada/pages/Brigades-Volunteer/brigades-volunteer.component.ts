import {
  Component,
  Input,
  OnInit
} from '@angular/core';

import { ConfirmationService, MessageService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { volunteer } from '../../../../models/Volunteer/Volunteer';
import { BrigadeVlServices } from './Services/BrigadeVlServices';

import { PersonDto } from '../../../person/interfaces/person-dto';
import { PersonFilter } from '../../../person/interfaces/person-filter';
import { PersonService } from '../../../person/services/person.service';

import { BrigadeVolunteerFilter } from '../../models/brigadaVolunteer-filter';
import { BrigadeService } from '../../services/brigade.service';
import { CreateBrigVolunterComponent } from './create-brig-volunter/create-brig-volunter.component';
@Component({
  selector: 'app-brigades-volunteer',
  templateUrl: './brigades-volunteer.component.html',
  styleUrl: './brigades-volunteer.component.sass'
})
export class BrigadesVolunteerComponent implements OnInit {

  @Input() queryRequest: BrigadeVolunteerFilter | undefined;
  @Input() isUpdateListDetails: boolean = false;
  @Input() searchFilter: any = {};
  loading: boolean = false;
  listaVoluntario: volunteer[] = [];
  Personastado: PersonDto[] = [];
  totalRows: number = 0;
  brigadeFiler!: BrigadeVolunteerFilter;
  personFiler!: PersonFilter;
  filteredPersonas: PersonDto[] = [];
  hasActiveVolunteer: boolean = false;
  openFilterPanel = true;
  entityFilterRequest: BrigadeVolunteerFilter | undefined;




  cambios: any;

  storedFilter: volunteer | undefined;
  constructor(
    private dialogService: DialogService,
    private BridagevlService: BrigadeVlServices,
    private messageService: MessageService,
    private confirmationService: ConfirmationService,
    private personService: PersonService,
    private brigadeService: BrigadeService,
  ) { }


  openCloseFilter() {
    this.openFilterPanel = !this.openFilterPanel;
  }
  getBrigadaRequestFilter(event: BrigadeVolunteerFilter) {
    this.entityFilterRequest = event;
  
    // Filtrar valores vacíos o nulos
    let beneficiarioFilter = Object.fromEntries(
      Object.entries(event).filter(
        ([key, value]) => value !== '' && value !== null
      )
    );
  
    if (Object.keys(beneficiarioFilter).length !== 0) {
      beneficiarioFilter = { ...beneficiarioFilter, offset: 0, take: 10 };
  
      this.BridagevlService //@ts-ignore
      .getAllBrigades(beneficiarioFilter).subscribe(
        (result) => {
          this.cambios = {
            listabeneficiarios: result.result,
            totalRows: result.length,
            loading: false,
          };
        },
        (error) => {
          console.error('API Error:', error);
        }
      );
    }
  }
  
/*
  getBrigadaRequestFilter(event: BrigadeVolunteerFilter) {
    this.entityFilterRequest = event;

    let beneficiarioFilter = Object.fromEntries(
      Object.entries(event).filter(
        ([key, value]) => value !== '' && value !== null
      )
    );

    if (Object.keys(beneficiarioFilter).length !== 0) {
      beneficiarioFilter = { ...beneficiarioFilter, offset: 0, take: 10 };

       console.log(beneficiarioFilter);

      this.BridagevlService
        //@ts-ignore
        .getAllBrigades(beneficiarioFilter)
        .subscribe(
          (result) => {
            this.cambios = {
              listabeneficiarios: result.result,
              totalRows: result.length,
              loading: false,
            };
          },
          (error) => {}
        );
        console.log(this.cambios);
    }
  }*/


  


  filterPersonas() {
    // Filtrar solo las personas que tienen el valor `volunteer` que deseas
    this.filteredPersonas = this.Personastado.filter(persona => persona.volunteer === true);
  }
 

  ngOnInit(): void {
   


  }


  NavigateToCreate() {
    const refdialog = this.dialogService
      .open(CreateBrigVolunterComponent, {
        header: 'Añadir voluntarios a una Brigada',
        width: 'auto',
        height: 'auto',
        data: {},
        contentStyle: { 'min-height': '140px', 'min-width': '150px' },
        baseZIndex: 10000,
      })
      .onClose.subscribe((result) => {
        this.isUpdateListDetails = true;
      
      });
      this.isUpdateListDetails = false;
  }
  
 
}

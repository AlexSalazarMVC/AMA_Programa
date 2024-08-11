
import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  SimpleChanges,
} from '@angular/core';
import { FormGroup } from '@angular/forms';
import { FormService } from '../../../../../shared/services/from.service'; 
import { BrigadeVolunteerFilter } from '../../../models/brigadaVolunteer-filter'; 
import { ChangeItemDropdown, ConfigurationDropdownProp, DynamicDataToDialog,ItemDropdown } from '../../../../../core/interfaces/ItemDropdown.models'; 
import { volunteer } from '../../../../../models/Volunteer/Volunteer';
import { AppModule } from "../../../../../app.module"; 
import { BrigadeVlServices } from '../Services/BrigadeVlServices';

@Component({
  selector: 'app-filter-brigades-volunteer',
  templateUrl: './filter-brigades-volunteer.component.html',
  styleUrl: './filter-brigades-volunteer.component.sass'
})
export class FilterBrigadesVolunteerComponent {


  itemsPersona!: ItemDropdown[];
  lsPersona!: DynamicDataToDialog;
  personaConfig!: ConfigurationDropdownProp;
  responsableConfig !: ConfigurationDropdownProp;
  onItemChanged(eventData: ChangeItemDropdown) {
    if (eventData && eventData.conf.Id === 'responsableBrigada') {
      const selectedData = eventData.data;
      this.formFilterConsult.get('volunteerId')?.setValue(selectedData.code); // Asigna el id del responsable
    
    }
  
  }
    @Output() queryEmitter = new EventEmitter<BrigadeVolunteerFilter>();
  
    @Input() openContentReceiver: boolean =false;
  
    IsOpen: boolean = false;
    formFilterConsult!: FormGroup;
    fomrBrigadeFilter!: BrigadeVolunteerFilter;
  
    constructor(private formService: FormService, private BridagevlServic:BrigadeVlServices ) {
  
    }
  
    ngOnInit(): void {
      // this.openContentReceiver=true
      this.clearFiltersEvent();
      this.buildForm();
      this.InitializerData();
    }
  
    ngOnChanges(changes: SimpleChanges): void {
      for (let propName in changes) {
        if (propName === 'openContentReceiver') {
          this.IsOpen = !this.IsOpen;
        }
      }
    }
  
    Buscar() {
     this.queryEmitter.emit(this.formFilterConsult.value);
      console.log(this.formFilterConsult.value);
      
    
    }
    
    clearFiltersEvent() {
      if (this.formFilterConsult) {
          this.formFilterConsult.reset();
          this.queryEmitter.emit({
              ...this.formFilterConsult.value,
              offset: 0,
              take: 10,
          });
      }
      this.itemsPersona = [];
  }
  
  
    closed() {
      // this.clearFiltersEvent();
      this.IsOpen = !this.IsOpen;
    }
    InitializerData() {
      this.lsPersona = { Params: [] };
      this.personaConfig = {
        Id: 'responsableBrigada',
        Name: 'Persona',
        Tooltip: 'Search Persona',
        Dataset: 'Persona',
        NameComponent: 'PersonDialogComponet'
      };
      this.itemsPersona = [];

    
    }
  
    buildForm() {
      this.fomrBrigadeFilter = {
      
        brigadeId: undefined,
        id:'' , 
        nombreBrigada: '',
        responsableBrigada: '',
        nombreVoluntario: '',
        
    volunteerId : undefined,
   
    offset: 0, 
    take: 10, 
    sort: '',
   
    

      };
      this.formFilterConsult = this.formService.createFormGroup<BrigadeVolunteerFilter>(
        this.fomrBrigadeFilter
      );
    }
  
  



}

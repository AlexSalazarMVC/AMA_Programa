import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BrigadaRoutingModule } from './brigada-routing.module';
import { SharedModule } from '../../shared/shared.module';
import { IndexBrigadesComponent } from './pages/index-brigades/index-brigades.component';
import { RippleModule } from 'primeng/ripple';
import { TagModule } from 'primeng/tag';
import { CreateOrEditBrigadesComponent } from './pages/create-or-edit-brigades/create-or-edit-brigades.component';
import { ListBrigadesComponent } from './pages/list-brigades/list-brigades.component';
import { FilterBrigadesComponent } from './pages/filter-brigades/filter-brigades.component';
import { BrigadeService } from './services/brigade.service';
import { BrigadesVolunteerComponent } from './pages/Brigades-Volunteer/brigades-volunteer.component';
import { ListBrigadesVlComponent } from './pages/Brigades-Volunteer/list-brigades-vl/list-brigades-vl.component'; 
import { FilterBrigadesVolunteerComponent } from './pages/Brigades-Volunteer/filter-brigades-volunteer/filter-brigades-volunteer.component'; 
@NgModule({
  declarations: [
    IndexBrigadesComponent,
 
    FilterBrigadesComponent,
    ListBrigadesComponent,
    CreateOrEditBrigadesComponent,
    BrigadesVolunteerComponent,
    ListBrigadesVlComponent,
    FilterBrigadesVolunteerComponent,
  ],
  imports: [
 
 
    BrigadaRoutingModule,
    SharedModule,
  ],
  exports: [
    BrigadaRoutingModule
  ],
  providers: [BrigadeService],
 
})
export class BrigadaModule {}

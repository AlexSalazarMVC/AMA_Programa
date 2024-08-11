import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IndexBrigadesComponent } from './pages/index-brigades/index-brigades.component';
import { CommonModule } from '@angular/common';
import { BrigadesVolunteerComponent } from './pages/Brigades-Volunteer/brigades-volunteer.component';
const routes: Routes = [

  { path: 'index', component: IndexBrigadesComponent},
  { path: 'voluntario', component: BrigadesVolunteerComponent}
];

@NgModule({
  imports: [CommonModule,RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BrigadaRoutingModule { }

import { Injectable, OnInit } from '@angular/core';
import { PersonDialogComponet } from '../shared/component/person-dialog-componet/person-dialog-componet.component';
import { BrigadeDialogComponent } from '../shared/component/brigade-dialog/brigade-dialog.component';
 import { CreateBrigVolunterComponent } from '../module/brigada/pages/Brigades-Volunteer/create-brig-volunter/create-brig-volunter.component';
 

@Injectable({
  providedIn: 'root'
})
export class DataSetComponentService  {
  COMPONETS: { [key: string]: any } ={
    "PersonDialogComponet":PersonDialogComponet,
    "BrigadeDialogComponent":BrigadeDialogComponent,
 "CreateBrigVolunterComponent":CreateBrigVolunterComponent 
  }
 get(name: string): any{
  return  this.COMPONETS[name];
 }
}

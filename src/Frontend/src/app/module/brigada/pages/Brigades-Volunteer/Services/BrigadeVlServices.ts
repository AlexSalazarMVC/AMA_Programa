import { Injectable } from '@angular/core';
import { environment } from '../../../../../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ToolsService } from '../../../../../services/tools.service'; 
import { ConfSystemServiceService } from '../../../../../data/conf-system-service.service';
import { Result, ResultData, ResultPaged } from '../../../../../core/interfaces/result';
import { Observable } from 'rxjs';
import { volunteer } from '../../../../../models/Volunteer/Volunteer';
import { BrigadeFilter } from '../../../models/brigada-filter.interface';
import { Brigadevolunteer } from '../../../../../models/Volunteer/BrigadeVolunteer';
import { BrigadeVolunteerFilter } from '../../../models/brigadaVolunteer-filter';

@Injectable({
  providedIn: 'root'
})
export class BrigadeVlServices {
  private apiUrl = environment.serverAma+ 'api/BrigadeVolunteer';

  constructor(private http: HttpClient,
    private tools:ToolsService,
    private config:ConfSystemServiceService) { }

  getAllBrigades(filterrequest:BrigadeVolunteerFilter): Observable<ResultPaged<volunteer>> {
    const params = this.tools.getHttpParams(filterrequest);
    return this.http.get<ResultPaged<volunteer>>(this.apiUrl, { params:params });
  }
  createBrigadeVolunteer(Brigade: Brigadevolunteer): Observable<Result> {
    return this.http.post<Result>(this.apiUrl, Brigade);
  }
  updateBrigade(brigade: Brigadevolunteer): Observable<Result> {
    return this.http.put<Result>(`${this.apiUrl}/${brigade.id}`, brigade);
  }
  deleteBrigade(id: number): Observable<Result> {
    return this.http.delete<Result>(`${this.apiUrl}/${id}`);
  }
  getBrigadeById(id: number): Observable<ResultData<volunteer>> {
    return this.http.get<ResultData<volunteer>>(`${this.apiUrl}/${id}`);
  }
  createBrigadeVolunteerLista(payload: any[]): Observable<Result> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<Result>(this.apiUrl+'/BulkInsert', payload,{ headers, responseType: 'text' as 'json' });
  }
}

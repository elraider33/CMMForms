import { CMM } from './../interfaces/cmm.interface';

import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class CMMService {
  cmms$: BehaviorSubject<CMM[]> = new BehaviorSubject<CMM[]>([]);
  cmm$: BehaviorSubject<CMM> = new BehaviorSubject<CMM>(null);

  constructor(private apiService: ApiService) { }

  getCMMs() {
    const subs$ = this.apiService.get<CMM[]>('Manuals')
    .subscribe(result => {
      this.cmms$.next(result);
    },console.log,() => {
      subs$.unsubscribe();
    });
  }
  createCMM(data){
    return this.apiService.post('Manuals',data);
  }

  updateCMM(id, data){
    return this.apiService.put(`Manuals/${id}`,data);
  }
  deleteCmm(id){
    return this.apiService.delete(`Manuals/${id}`);
  }
  getCMM(id: string) {
    const subs$ = this.apiService.get<CMM>(`Manuals/${id}`)
    .subscribe(result => {
      this.cmm$.next(result);
    },console.log,() => {
      subs$.unsubscribe();
    });
  }

  getByRoles(){
    return this.apiService.get('Manuals/Roles');
  }
}

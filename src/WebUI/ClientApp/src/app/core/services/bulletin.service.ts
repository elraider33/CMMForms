import { Bulletin } from './../interfaces/bulletin.interface';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class BulletinService {
  bulletins$: BehaviorSubject<Bulletin[]> = new BehaviorSubject<Bulletin[]>([]);
  bulletin$: BehaviorSubject<Bulletin> = new BehaviorSubject<Bulletin>(null);

  constructor(private apiService: ApiService) { }

  getBulletins() {
    const subs$ = this.apiService.get<Bulletin[]>('Bulletins')
    .subscribe(result => {
      this.bulletins$.next(result);
    },
      error=> console.log(error), () => {
      subs$.unsubscribe();
    });
  }
  createBulletin(data){
    return this.apiService.post('Bulletins',data);
  }

  updateBulletin(id, data){
    return this.apiService.put(`Bulletins/${id}`,data);
  }
  deleteBulletin(id){
    return this.apiService.delete(`Bulletins/${id}`);
  }
  getBulletin(id: string) {
    const subs$ = this.apiService.get<Bulletin>(`Bulletins/${id}`)
    .subscribe(result => {
      this.bulletin$.next(result)
    },console.log,() => {
      subs$.unsubscribe();
    });
  }
  getByRoles(){
    return this.apiService.get('Bulletins/Roles');
  }
}

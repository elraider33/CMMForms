import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Entity } from '../interfaces/entity.interface';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class EntityService {
  entities$: BehaviorSubject<Entity[]> = new BehaviorSubject<Entity[]>([]);
  constructor(private api: ApiService) { }

  getEntities() {
    const subs$ =  this.api.get<Entity[]>('Entities')
    .subscribe(result => {
      this.entities$.next(result)
    },console.log,() => {
      subs$.unsubscribe();
    });
  }
  createEntity(data: FormData) {
    return this.api.post('Entities',data);
  }
}

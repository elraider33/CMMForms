import { ApiService } from './api.service';
import { Injectable } from '@angular/core';
import Role from '../interfaces/Role.interface';

@Injectable({
  providedIn: 'root'
})
export class RoleService {

  constructor(private api: ApiService) { }

  getRoles() {
    return this.api.get<Role[]>('Roles');
  }
  getById(id: string) {
    return this.api.get<Role>(`Roles/${id}`);
  }
  create(role: Role) {
    console.log(role);
    return this.api.post('Roles', role );
  }
  update(id: string, role: Role) {
    return this.api.put(`Roles/${id}`, role);
  }
  delete(id: string) {
    return this.api.delete(`Roles/${id}`);
  }
}

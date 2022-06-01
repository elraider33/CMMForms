import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { map } from 'rxjs/operators';
import Role from 'src/app/core/interfaces/Role.interface';
import { AuthService } from 'src/app/core/services/auth.service';
import { RoleService } from 'src/app/core/services/role.service';
import { UiService } from 'src/app/core/services/ui.service';
import { returnFilter } from "../../../../core/utils/utils";


@Component({
  selector: 'app-role-index',
  templateUrl: './role-index.component.html',
  styleUrls: ['./role-index.component.scss']
})
export class RoleIndexComponent implements OnInit, AfterViewInit  {
  roles: Role[];
  dataSource: MatTableDataSource<Role>;
  displayedColumns = [
    "name",
    "description",
    "emailAddresses"
  ];
  loading: boolean = true;
  isAdmin: boolean;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  roleSubspcription: Subscription

  filterValues: any = {
    role: '',
    description:'',
  };
  roleFilter = new FormControl('');
  descriptionFilter = new FormControl('');

  constructor(
    private roleService: RoleService,
    private authService: AuthService,
    private ui: UiService) {

  }
  ngAfterViewInit(): void {
    this.ui.spin$.next(true);
    this.isAdmin = this.authService.isAdmin();
    this.roleSubspcription = this.roleService.getRoles()
    .subscribe(data => {
      console.log(data);
      if(data.length){
        this.roles = data;
        this.dataSource = new MatTableDataSource(this.roles);
        this.paginator.showFirstLastButtons = true;
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        this.dataSource.filterPredicate = this.createFilter();
        this.ui.spin$.next(false);
      }
    },
    error => {
      console.log(error);
      this.ui.spin$.next(false);
    },
    () => {

    } );
  }
  
  ngOnInit() {
    this.fieldListener();
  }

  private fieldListener() {
    this.roleFilter.valueChanges
      .subscribe(
        role => {
          if(!role){
            this.dataSource.filter = '';
          }else{
            this.filterValues.role = role.trim().toLowerCase();
            this.dataSource.filter = JSON.stringify(this.filterValues);
          }
        }
      )
    this.descriptionFilter.valueChanges
      .subscribe(
        description => {
          if(!description){
            this.dataSource.filter = '';
          }else {
            this.filterValues.description = description.trim().toLowerCase();
            this.dataSource.filter = JSON.stringify(this.filterValues);
          }
        }
      )
  }
  clearFilter() {
    this.roleFilter.setValue('');
    this.descriptionFilter.setValue('');
    this.dataSource.filter = '';
  }
  displayArray(array){
    return array.length ? array.join(';').substring(0,50) + '...' : '';
  }
  private createFilter(): (role: Role, filter: string) => boolean {
    const filterFunction = (role, filter): boolean => {
      const searchTerms = JSON.parse(filter);

      const roleFilter = returnFilter(searchTerms.role, role.name);
      const descriptionfilter = returnFilter(searchTerms.description, role.description);
      

      return roleFilter && descriptionfilter;

      
    };
    return filterFunction;
  }
}

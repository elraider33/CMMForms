import { Subscription } from 'rxjs';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { Entity } from 'src/app/core/interfaces/entity.interface';
import { EntityService } from 'src/app/core/services/entity.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { toFormData } from 'src/app/core/utils/utils';
import { UiService } from 'src/app/core/services/ui.service';
import { MatSnackBar } from '@angular/material/snack-bar';


@Component({
  selector: 'app-entities-index',
  templateUrl: './entities-index.component.html',
  styleUrls: ['./entities-index.component.scss']
})
export class EntitiesIndexComponent implements OnInit, AfterViewInit {
  subs: Subscription
  entityForm = new FormGroup({
    description: new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(50)])
  });
  entities: Entity[];
  dataSource: MatTableDataSource<Entity>;
  displayedColumns = [
    'id',
    'description'
  ];
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  constructor(
    private entityService: EntityService,
    private readonly snackBar: MatSnackBar,
    private ui: UiService
  ) {
    
   }
   get description() {return this.entityForm.get('description');}
  ngAfterViewInit(): void {
    this.subs = this.entityService.entities$.subscribe(data => {
        if (data.length) {
          console.log(data);
          this.entities = data;
          this.dataSource = new MatTableDataSource(this.entities);
          this.paginator.showFirstLastButtons = true;
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
        }
      },
      error => {
        console.log(error);
        this.snackBar.open(`Error Loading Data`,
          '',
          {
            duration: 2 * 1000
          });
        this.ui.spin$.next(false);
      },
      () => {

      });
  }

  ngOnInit(): void {
    this.entityService.getEntities();
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    console.log(filterValue);
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
  onSubmit(){
    const formData = toFormData(this.entityForm.value);
    if(this.entityForm.valid){
      this.ui.spin$.next(true);
      const subs$ = this.entityService.createEntity(formData).subscribe(data => {
        this.entityForm.markAsPristine();
        this.entityForm.markAsUntouched();
        this.entityForm.reset();
        this.entityService.getEntities();
      },console.log,
      () => {
        this.ui.spin$.next(false);
        subs$.unsubscribe();
      });
    }
  }
}

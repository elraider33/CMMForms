import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, HostBinding, OnInit, AfterViewInit, ChangeDetectionStrategy, ViewChild, ChangeDetectorRef } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Bulletin } from 'src/app/core/interfaces/bulletin.interface';
import { GroupBy } from 'src/app/core/interfaces/groupby.interface';
import { BulletinService } from 'src/app/core/services/bulletin.service';
import { UiService } from 'src/app/core/services/ui.service';
import { isBulletin, isGroupBy } from 'src/app/core/utils/utils';
import { MatSort } from '@angular/material/sort';
@Component({
  selector: 'app-bulletins-by-role',
  templateUrl: './bulletins-by-role.component.html',
  styleUrls: ['./bulletins-by-role.component.scss'],
  animations: [
    trigger('indicatorRotate', [
      state('collapsed', style({transform: 'rotate(0deg)'})),
      state('expanded', style({transform: 'rotate(180deg)'})),
      transition('expanded <=> collapsed',
        animate('225ms cubic-bezier(0.4,0.0,0.2,1)')
      ),
    ])
  ]
})
export class BulletinsByRoleComponent implements OnInit, AfterViewInit {
  ELEMENT_DATA: (Bulletin | GroupBy)[] = [];
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  displayedColumns = [
    "sbno",
    "entity",
    "type",
    "modelNumber",
    "customer"
  ];
  dataSource: MatTableDataSource<Bulletin | GroupBy>;
  allData: any;
  @HostBinding('attr.aria-expanded') ariaExpanded = true;
  FilterValue = "";
  constructor(
    private readonly changeDetectorRef: ChangeDetectorRef,
    private readonly bulletinService: BulletinService,
    private readonly ui: UiService
    ) { }
  ngAfterViewInit(): void {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
    this.dataSource.filterPredicate = (data, filter: string) => {
      if (isBulletin(data)) {
        return data.sbSearch?.toLowerCase().startsWith(filter);
      }
      if (isGroupBy(data)) {
        return data.initial.toLocaleLowerCase().startsWith(filter);
      }
    };
      this.ui.spin$.next(true);
      this.bulletinService.getByRoles().subscribe(response => {
        this.allData = response;
        this.setData(response);
      },
      error => {
        console.log(error);
        this.ui.spin$.next(false);
      },
      () => {

      } );
    }

  ngOnInit(): void {
    this.dataSource = new MatTableDataSource<Bulletin | GroupBy>();
   
  }

  isGroup(index, item): boolean{
    return item.isGroupBy;
  }
  onHeaderClicl(row){
    if(row.expanded){
      row.expanded = false;
      console.log(this.ELEMENT_DATA.indexOf(row));
      //filtrar element data por sb search by role
    }else{
      //agregar todos los elementos otra vez?
      row.expanded = true;
    }
    // this.dataSource.filter = performance.now().toString();
  }
  setFilter(event: Event) {
    this.FilterValue = (event.target as HTMLInputElement).value;
  }

  applyFilter() {

    this.dataSource.filter = this.FilterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
  setData(response){
    const keys = Object.keys(response);
    for(var key of keys){
      this.ELEMENT_DATA.push({initial: key, isGroupBy: true, expanded: response[key].length});
      const data = response[key];
      data.forEach(b => {
        // agregar al sb search los cambos a filtrar incluido el rol
        b.sbSearch = key;
        this.ELEMENT_DATA.push(b);
      });
    }
    this.dataSource.data = this.ELEMENT_DATA;
    this.changeDetectorRef.detectChanges();
    this.ui.spin$.next(false);
    
    
  }
}


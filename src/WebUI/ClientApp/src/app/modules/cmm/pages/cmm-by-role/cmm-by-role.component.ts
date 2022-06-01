import { AfterViewInit, ChangeDetectionStrategy, ViewChild } from '@angular/core';
import { Input } from '@angular/core';
import { ChangeDetectorRef } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { from } from 'rxjs';
import { Observable } from 'rxjs';
import { CMM } from 'src/app/core/interfaces/cmm.interface';
import { GroupBy } from 'src/app/core/interfaces/groupby.interface';
import { CMMService } from 'src/app/core/services/cmm.service';
import { UiService } from 'src/app/core/services/ui.service';
import { isCmm, isGroupBy } from 'src/app/core/utils/utils';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'app-cmm-by-role',
  templateUrl: './cmm-by-role.component.html',
  styleUrls: ['./cmm-by-role.component.scss']
})
export class CmmByRoleComponent implements OnInit, AfterViewInit {
  @Input() public data$: Observable<(CMM | GroupBy)[]>;
  ELEMENT_DATA: (CMM | GroupBy)[]=[];
  displayedColumns = [
    "cmmNumber",
    "entity",
    "customer",
    "aircraft",
    "model"
  ];
  FilterValue = "";
  dataSource: MatTableDataSource<CMM | GroupBy>;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  constructor(
    private changeDetectorRef: ChangeDetectorRef,
    private cmmService: CMMService,
    private ui: UiService) { }
    ngAfterViewInit(): void {
      this.ui.spin$.next(true);
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.paginator;
      
      this.dataSource.filterPredicate = (data, filter: string) => {
        if (isCmm(data)) {
          return data.unId?.toLowerCase().startsWith(filter);
        } if (isGroupBy(data)) {
          return data.initial.toLocaleLowerCase().startsWith(filter);
        }
      }
      this.cmmService.getByRoles().subscribe(response => {
        this.setData(response);
        this.ui.spin$.next(false);
      },
      error => {
        console.log(error);
        this.ui.spin$.next(false);
      },
      () => {

      } );
     
    }

  ngOnInit(): void {
    this.dataSource = new MatTableDataSource<CMM | GroupBy>();
  }

  isGroup(index, item): boolean{
    return item.isGroupBy;
  }

  setFilter(event: Event) {
    this.FilterValue = (event.target as HTMLInputElement).value;
  }

  applyFilter() {
    
    this.ui.spin$.next(true);
    this.dataSource.filter = this.FilterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
    this.ui.spin$.next(false);
  }

  setData(response){
    var keys = Object.keys(response);
    for(var key of keys){
      this.ELEMENT_DATA. push({initial: key, isGroupBy: true, expanded: response[key].length});
      const data = response[key];
      data.forEach(b => {
        b.unId = key;
        this.ELEMENT_DATA.push(b);
      });
    }
    this.dataSource.data = this.ELEMENT_DATA;
    this.changeDetectorRef.detectChanges();
    
    
  }

}

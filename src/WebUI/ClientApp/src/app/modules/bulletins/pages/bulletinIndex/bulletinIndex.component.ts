import { map, finalize, timeout } from 'rxjs/operators';
import { AfterViewChecked, AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { Subscription } from 'rxjs';
import { Bulletin } from 'src/app/core/interfaces/bulletin.interface';
import { BulletinService } from 'src/app/core/services/bulletin.service';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import { AuthService } from 'src/app/core/services/auth.service';
import { UiService } from 'src/app/core/services/ui.service';
import { FormControl } from '@angular/forms';
import { returnFilter } from "../../../../core/utils/utils";

@Component({
  selector: 'app-bulletinIndex',
  templateUrl: './bulletinIndex.component.html',
  styleUrls: ['./bulletinIndex.component.scss']
})
export class BulletinIndexComponent implements OnInit, AfterViewInit  {
  bulletins: Bulletin[];
  dataSource: MatTableDataSource<Bulletin>;
  displayedColumns = [
    "sbno",
    "entity",
    "type",
    "modelNumber",
    "customer",
    "initialDate",
    "manualRev",
    "revDate",
    "view",
    "description",
    "aircraft",
    "model",
    "jobNumber"
  ];
  isAdmin: boolean = false;
  loading: boolean = true;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  bulletinsSubspcription: Subscription
  filterValues: any = {
    sbno: '',
    customer:'',
    aircraft: '',
    model: ''
  };
  sbnoFilter = new FormControl('');
  customerFilter = new FormControl('');
  aircraftFilter = new FormControl('');
  modelFilter = new FormControl('');

  constructor(
    private bulletinService: BulletinService,
    private authService: AuthService,
    private ui : UiService,
  ) {
    
  }
  ngAfterViewChecked(): void {
    
  }
  
  ngAfterViewInit(): void {
    this.ui.spin$.next(true);
    console.log(this.isAdmin);
    this.bulletinService.getBulletins();
    this.bulletinsSubspcription = this.bulletinService.bulletins$
      .pipe(
        map(data => {
          data.forEach(d => d.sbSearch = `${d.sbno} ${d.customer} ${d.aircraft} ${d.model}`);
          return data;
        }),
        finalize(() => {

        }),
        // timeout(1000)
      ).subscribe(data => {
        console.log(data);
        if (data.length) {
          this.bulletins = data;
          this.dataSource = new MatTableDataSource(this.bulletins);
          this.paginator.showFirstLastButtons = true;
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
          this.dataSource.filterPredicate = this.createFilter();
          // this.dataSource.filterPredicate = (data, filter: string) => {
          //   return data.sbSearch?.toLowerCase().includes(filter);
          // }
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
    this.isAdmin = this.authService.isAdmin();
  }
  // applyFilter(event: Event) {
  //   const filterValue = (event.target as HTMLInputElement).value;

  //   this.dataSource.filter = filterValue.trim().toLowerCase();

  //   if (this.dataSource.paginator) {
  //     this.dataSource.paginator.firstPage();
  //   }
  // }

  private fieldListener() {
    this.sbnoFilter.valueChanges
      .subscribe(
        sbno => {
          this.filterValues.sbno = sbno.trim().toLowerCase();
          this.dataSource.filter = JSON.stringify(this.filterValues);
        }
      );
    this.customerFilter.valueChanges
      .subscribe(
        customer => {
          this.filterValues.customer = customer.trim().toLowerCase();
          this.dataSource.filter = JSON.stringify(this.filterValues);
        }
      );
    this.aircraftFilter.valueChanges
      .subscribe(
        aircraft => {
          this.filterValues.aircraft = aircraft.trim().toLowerCase();
          this.dataSource.filter = JSON.stringify(this.filterValues);
        }
      );
    this.modelFilter.valueChanges
      .subscribe(
        model => {
          this.filterValues.model = model.trim().toLowerCase();
          this.dataSource.filter = JSON.stringify(this.filterValues);
        }
      );
  }

  clearFilter() {
    this.sbnoFilter.setValue('');
    this.customerFilter.setValue('');
    this.aircraftFilter.setValue('');
    this.modelFilter.setValue('');
    this.dataSource.filter = '';
  }

  private createFilter(): (sb: Bulletin, filter: string) => boolean {
    const filterFunction = (sb, filter): boolean => {
      const searchTerms = JSON.parse(filter);
     
      const sbnoFilter = returnFilter(searchTerms.sbno , sb.sbno);
      const customerfilter = returnFilter(searchTerms.customer, sb.customer);
      const acfilter = returnFilter(searchTerms.aircraft, sb.aircraft);
      const modelfilter = returnFilter(searchTerms.model, sb.model);

      return sbnoFilter && customerfilter && acfilter && modelfilter;
    };
    return filterFunction;
  }
  
}

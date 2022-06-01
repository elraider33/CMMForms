import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { map } from 'rxjs/operators';
import { CMM } from 'src/app/core/interfaces/cmm.interface';
import { AuthService } from 'src/app/core/services/auth.service';
import { CMMService } from 'src/app/core/services/cmm.service';
import { UiService } from 'src/app/core/services/ui.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { returnFilter } from "../../../../core/utils/utils";

@Component({
  selector: 'app-cmm-index',
  templateUrl: './cmm-index.component.html',
  styleUrls: ['./cmm-index.component.scss']
})
export class CmmIndexComponent implements OnInit, AfterViewInit  {
  cmms: CMM[];
  dataSource: MatTableDataSource<CMM>;
  displayedColumns = [
    "cmmNumber",
    "entity",
    "customer",
    "aircraft",
    "model",
    "available",
    "manualRev",
    "revDate",
    "comments"
  ];
  loading: boolean = true;
  isAdmin: boolean;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  cmmSubspcription: Subscription;

  filterValues: any = {
    cmmNumber: '',
    customer:'',
    aircraft: '',
    model: ''
  };
  cmmNumerFilter = new FormControl('');
  customerFilter = new FormControl('');
  aircraftFilter = new FormControl('');
  modelFilter = new FormControl('');

  constructor(
    private readonly cmmService: CMMService,
    private readonly authService: AuthService,
    private readonly snackBar: MatSnackBar,
    private readonly ui: UiService) {
  }
  ngAfterViewInit(): void {
    this.ui.spin$.next(true);
    this.cmmService.getCMMs();
    this.cmmSubspcription = this.cmmService.cmms$
    .pipe(map(data => {
      data.forEach(d => d.unId = `${d.customer} ${d.aircraft} ${d.model}`);
      return data;
    }))
    .subscribe(data => {
      if(data.length){
        this.cmms = data;
        this.dataSource = new MatTableDataSource(this.cmms);
        this.paginator.showFirstLastButtons = true;
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        this.dataSource.filterPredicate = this.createFilter();
        // this.dataSource.filterPredicate = (data, filter: string) => {
          //   return data.unId?.toLowerCase().includes(filter);
          // }
          this.ui.spin$.next(false);
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

      } );
    }
    
    ngOnInit() {
      this.fieldListener();
      this.isAdmin = this.authService.isAdmin();
  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    const filterArray = filterValue.split(' ');
    if(filterArray.length === 1){
      this.filterValues.cmmNumber = filterArray[0].trim().toLowerCase();
    }
    if(filterArray.length === 2){
      this.filterValues.customer = filterArray[1].trim().toLowerCase();
    }
    if(filterArray.length === 3){
      this.filterValues.aircraft = filterArray[2].trim().toLowerCase();
    }
    if(filterArray.length === 4){
      this.filterValues.model = filterArray[3].trim().toLowerCase();
    }
    this.dataSource.filter = JSON.stringify(this.filterValues);

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  private fieldListener() {
    this.cmmNumerFilter.valueChanges
      .subscribe(
        cmmNumber => {
          this.filterValues.cmmNumber = cmmNumber.trim().toLowerCase();
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
    this.cmmNumerFilter.setValue('');
    this.customerFilter.setValue('');
    this.aircraftFilter.setValue('');
    this.modelFilter.setValue('');
  }

  private createFilter(): (sb: CMM, filter: string) => boolean {
    const filterFunction = (sb, filter): boolean => {
      const searchTerms = JSON.parse(filter);

      const cmmFilter = returnFilter(searchTerms.cmmNumber , sb.cmmNumber);
      const customerfilter = returnFilter(searchTerms.customer , sb.customer);
      const acfilter = returnFilter(searchTerms.aircraft , sb.aircraft);
      const modelfilter = returnFilter(searchTerms.model , sb.model);

      return cmmFilter && customerfilter && acfilter && modelfilter;
    };
    return filterFunction;
  }
  
}

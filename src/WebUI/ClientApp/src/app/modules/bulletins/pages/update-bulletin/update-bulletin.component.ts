import { Observable, Subscription } from "rxjs";
import { BulletinService } from "src/app/core/services/bulletin.service";
import {
  Component,
  OnInit,
  OnDestroy,
  ViewChild,
  ViewChildren,
  ElementRef,
} from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { Bulletin } from "src/app/core/interfaces/bulletin.interface";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { downloadFile, toFormData, stringToArray } from "src/app/core/utils/utils";
import { EntityService } from "src/app/core/services/entity.service";
import { Entity } from "src/app/core/interfaces/entity.interface";
import { FileService } from 'src/app/core/services/file.service';
import { CMMService } from 'src/app/core/services/cmm.service';
import { CMM } from 'src/app/core/interfaces/cmm.interface';
import { map, startWith, switchMapTo } from 'rxjs/operators';
import { UiService } from 'src/app/core/services/ui.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: "app-update-bulletin",
  templateUrl: "./update-bulletin.component.html",
  styleUrls: ["./update-bulletin.component.scss"],
})
export class UpdateBulletinComponent implements OnInit, OnDestroy {
  subs: Subscription;
  bulletin: Bulletin = null;
  bulletinForm = new FormGroup({
    requestedBy: new FormControl(),
    requestedOn: new FormControl(new Date(), Validators.required),
    entity: new FormControl('', Validators.required),
    published: new FormControl('', Validators.required),
    file: new FormControl(),
    sbno: new FormControl('', Validators.required),
    type: new FormControl('', Validators.required),
    modelNumber: new FormControl('', Validators.required),
    initialDate: new FormControl(),
    customer: new FormControl(),
    manualRev: new FormControl(),
    revDate: new FormControl(new Date(), Validators.required),
    description: new FormControl(),
    model: new FormControl(),
    aircraft: new FormControl(),
    cmm: new FormControl('', Validators.required),
    jobNumber: new FormControl('', Validators.required),
    repairStationNumber: new FormControl(),
    seatPartNumbers: new FormControl(),
    comments: new FormControl(),
    roles: new FormControl('', Validators.required),
  });
  entities: Entity[];
  entitySubs: Subscription;
  cmms: string[];
  cmm:string;
  cmmList = new FormControl();
  filteredOptions: Observable<string[]>;
  seatPartNumbers: string;
  roles: string;
  rolesFormControl = new FormControl('', Validators.required);
  cmmFormControl = new FormControl('', Validators.required);
  @ViewChild("fileInput") fileInput: ElementRef;
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private bulletinService: BulletinService,
    private entityService: EntityService,
    private fileService: FileService,
    private snackBar: MatSnackBar,
    private ui : UiService,
  ) {
    this.entitySubs = this.entityService.entities$.subscribe((data) => {
      if (data.length) {
        this.entities = data;
      }
    });
    this.subs = this.bulletinService.bulletin$.subscribe((result) => {
      if (result) {
        this.bulletin = result;
        this.bulletinForm.setValue({
          requestedBy: this.bulletin.requestedBy,
          requestedOn: this.bulletin.requestedOn,
          entity: this.bulletin.entity,
          published: this.bulletin.published,
          sbno: this.bulletin.sbno,
          type: this.bulletin.type,
          modelNumber: this.bulletin.modelNumber,
          initialDate: this.bulletin.initialDate,
          customer: this.bulletin.customer,
          manualRev: this.bulletin.manualRev,
          revDate: this.bulletin.revDate,
          description: this.bulletin.description,
          model: this.bulletin.model,
          aircraft: this.bulletin.aircraft,
          cmm: this.bulletin.cmm ? this.bulletin.cmm.join('\r\n') : null,
          jobNumber: this.bulletin.jobNumber,
          repairStationNumber: this.bulletin.repairStationNumber === 'null' ? '' : this.bulletin.repairStationNumber,
         
          seatPartNumbers: this.bulletin.seatPartNumbers,
          comments: this.bulletin.comments==='null'? '' : this.bulletin.comments,
          roles: this.bulletin.roles ? this.bulletin.roles.join('\r\n') : null,
          file: this.bulletin.file?.filename || "",
        });
        this.seatPartNumbers = this.bulletin.seatPartNumbers.join("\r\n");
        this.roles = this.bulletin.roles.join("\r\n");
        this.cmm =  this.bulletin.cmm.join("\r\n");
        console.log(this.seatPartNumbers, this.roles);
        console.log(this.bulletin);
        this.ui.spin$.next(false);
        // if (this.bulletin.file !== null)
        //   this.fileInput.nativeElement.value = this.bulletin.file.filename;
      }
    });
  }
  ngOnDestroy(): void {
    this.entitySubs.unsubscribe();
    this.subs.unsubscribe();
  }

  ngOnInit(): void {
    this.ui.spin$.next(true);
    const id = this.route.snapshot.paramMap.get("id");
    this.entityService.getEntities();
    this.bulletinService.getBulletin(id);
    this.filteredOptions = this.cmmList.valueChanges.pipe(
      startWith(''),
      map(value => this._filter(value))
    );
  }
  private _filter(value: string): string[] {
    console.log(value);
    const filterValue = value.toLowerCase();

    return this.cmms.filter(cmm => cmm.toLowerCase().includes(filterValue));
  }
  onFileChange(event) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      this.bulletinForm.patchValue({
        file: file,
      });
    }
  }
  cmmChange(event){
    this.bulletinForm.patchValue({
      cmm: event.target.value ? event.target.value.split(/\n\r?/g) : null
    });
  }
  seatPartChange(event) {
    this.bulletinForm.patchValue({
      seatPartNumbers: event.target.value ? event.target.value.split(/\n\r?/g) : null
    });
  }
  rolesChange(event) {
    this.bulletinForm.patchValue({
      roles: event.target.value ? event.target.value.split(/\n\r?/g) : null
    });
  }
  onSubmit() {
    if (this.bulletinForm.valid) {
      this.ui.spin$.next(true);
      this.bulletinForm.patchValue({
        roles: stringToArray(this.bulletinForm.controls['roles'].value),
        seatPartNumbers: stringToArray(this.bulletinForm.controls['seatPartNumbers'].value),
        cmm: stringToArray(this.bulletinForm.controls['cmm'].value)
      });
      const formData = toFormData(this.bulletinForm.value);
      const subs$ = this.bulletinService
        .updateBulletin(this.bulletin.id, formData)
        .subscribe(
          (data) => {
            this.bulletinForm.reset();
            console.log(data);
            this.snackBar.open(`Service bulletin was Updated`,
              '',
              {
                duration: 2 * 1000
              });
            this.router.navigateByUrl('/modules/service-bulletins/' + this.bulletin.id + '&reload=true');
          },
          error => {
            console.log(error);
            this.ui.spin$.next(false);
          },
          () => {
            this.ui.spin$.next(false);
            subs$.unsubscribe();
          }
        );
    }
  }
  downloadDocument() {
    this.fileService.downloadDocument(this.bulletin.file?.filesId).subscribe(
      (data) => {
        console.log(data);
        downloadFile(data, this.bulletin.file?.filename);
      },
      (error) => {
        console.log("ERROR", error);
      }
    );
  }
  selectCMM(event){
    console.log(event);
  }
}

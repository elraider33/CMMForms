import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { CMM } from 'src/app/core/interfaces/cmm.interface';
import { Entity } from 'src/app/core/interfaces/entity.interface';
import { CMMService } from 'src/app/core/services/cmm.service';
import { EntityService } from 'src/app/core/services/entity.service';
import { FileService } from 'src/app/core/services/file.service';
import { UiService } from 'src/app/core/services/ui.service';
import { downloadFile, toFormData, stringToArray } from 'src/app/core/utils/utils';

@Component({
  selector: 'app-update-cmm',
  templateUrl: './update-cmm.component.html',
  styleUrls: ['./update-cmm.component.scss']
})
export class UpdateCmmComponent implements OnInit, OnDestroy {
  subs: Subscription;
  cmm: CMM = null;
  cmmForm = new FormGroup({
    requestedBy: new FormControl('', Validators.required),
    entity: new FormControl('', Validators.required),
    published: new FormControl('', Validators.required),
    file: new FormControl(),
    customer: new FormControl('', Validators.required),
    cmmNumber: new FormControl('', Validators.required),
    initialDate: new FormControl(),
    model: new FormControl(),
    manualRev: new FormControl(),
    revDate: new FormControl(),
    aircraft: new FormControl(),
    jobNo: new FormControl('', Validators.required),
    incorporatedSeatAssemblies: new FormControl(),
    serviceBulletins: new FormControl(),
    reference: new FormControl(),
    aircraftInstallation: new FormControl('', Validators.required),
    config: new FormControl('', Validators.required),
    trimFinish: new FormControl('', Validators.required),
    comments: new FormControl(),
    roles: new FormControl('', Validators.required),
    seatPartNumbers: new FormControl(),
    seatPartNumbersTSO: new FormControl('', Validators.required),
  });
  @ViewChild('fileInput') fileInput: ElementRef;
  tsoFormControl = new FormControl('', Validators.required);
  rolesFormControl = new FormControl('', Validators.required);
  entities: Entity[];
  entitySubs: Subscription;
  incorporatedSeatAssemblies: string;
  serviceBulletins: string;
  reference: string;
  roles: string;
  seatPartNumbers: string;
  seatPartNumbersTSO: string;
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private cmmService: CMMService,
    private ui: UiService,
    private fileService: FileService,
    private readonly snackBar: MatSnackBar,
    private entityService: EntityService,) {
      this.entitySubs = this.entityService.entities$.subscribe((data) => {
        if (data.length) {
          this.entities = data;
        }
      });
      this.subs = this.cmmService.cmm$.subscribe(result => {
        if(result){
          this.cmm = result;
          this.cmmForm.patchValue({
            requestedBy: this.cmm.requestedBy,
            entity: this.cmm.entity,
            published: this.cmm.published,
            customer: this.cmm.customer,
            cmmNumber: this.cmm.cmmNumber,
            initialDate: this.cmm.initialDate,
            model: this.cmm.model,
            manualRev: this.cmm.manualRev,
            revDate: this.cmm.revDate,
            aircraft: this.cmm.aircraft,
            jobNo: this.cmm.jobNo,
            vendor: this.cmm.vendor,
            engineer: this.cmm.engineer,
            rfq: this.cmm.rfq,
            pm: this.cmm.pm,
            incorporatedSeatAssemblies: this.cmm.incorporatedSeatAssemblies.join('\r\n'),
            serviceBulletins: this.cmm.serviceBulletins.join('\r\n'),
            reference: this.cmm.reference.join('\r\n'),
            aircraftInstallation: this.cmm.aircraftInstallation,
            config: this.cmm.config,
            trimFinish: this.cmm.trimFinish,
            comments: this.cmm.comments,
            documentAvailable: this.cmm.documentAvailable,
            roles: this.cmm.roles.join('\r\n'),
            seatPartNumbers: this.cmm.seatPartNumbers.join('\r\n'),
            seatPartNumbersTSO: this.cmm.seatPartNumbersTSO.join('\r\n'),
          });
          this.incorporatedSeatAssemblies = this.cmm.incorporatedSeatAssemblies.join('\r\n'),
          this.serviceBulletins = this.cmm.serviceBulletins.join('\r\n'),
          this.reference = this.cmm.reference.join('\r\n'),
          this.roles = this.cmm.roles.join('\r\n'),
          this.seatPartNumbers = this.cmm.seatPartNumbers.join('\r\n'),
          this.seatPartNumbersTSO = this.cmm.seatPartNumbersTSO.join('\r\n'),
          this.ui.spin$.next(false);
        }
      });
    }
  ngOnDestroy(): void {
    this.entitySubs.unsubscribe();
  }

  ngOnInit(): void {
    this.ui.spin$.next(true);
    const id = this.route.snapshot.paramMap.get("id");
    this.entityService.getEntities();
    this.cmmService.getCMM(id);
  }

  onFileChange(event) {
      if (event.target.files.length > 0) {
        const file = event.target.files[0];
        this.cmmForm.patchValue({
          file: file
        });
      }
  }
  incorporatedSeatAssembliesChange(event){
    this.cmmForm.patchValue({
      incorporatedSeatAssemblies: event.target.value ? event.target.value.split(/\n\r?/g) : null
    });
  }
  serviceBulletinsChange(event){
    this.cmmForm.patchValue({
      serviceBulletins: event.target.value ? event.target.value.split(/\n\r?/g) : null
    });
  }
  referenceChange(event){
    this.cmmForm.patchValue({
      reference: event.target.value ? event.target.value.split(/\n\r?/g) : null
    });
  }
  seatPartNumbersTSOChange(event){
    this.cmmForm.patchValue({
      seatPartNumbersTSO: event.target.value? event.target.value.split(/\n\r?/g): null
    });
  }
  seatPartNumbersChange(event){
    this.cmmForm.patchValue({
      seatPartNumbers: event.target.value ? event.target.value.split(/\n\r?/g) : null
    });
  }
  rolesChange(event){
    this.cmmForm.patchValue({
      roles: event.target.value ? event.target.value.split(/\n\r?/g) :null
    });
  }

  downloadDocument() {
    this.fileService.downloadDocument(this.cmm.file?.filesId).subscribe(
      (data) => {
        console.log(data);
        downloadFile(data, this.cmm.file?.filename);
      },
      (error) => {
        console.log("ERROR", error);
      }
    );
  }

  

  onSubmit() {
    if (this.cmmForm.valid) {
      this.ui.spin$.next(true);
      this.cmmForm.patchValue({
        roles: stringToArray(this.cmmForm.controls['roles'].value),
        seatPartNumbers: stringToArray(this.cmmForm.controls['seatPartNumbers'].value),
        seatPartNumbersTSO: stringToArray(this.cmmForm.controls['seatPartNumbersTSO'].value),
        reference: stringToArray(this.cmmForm.controls['reference'].value),
        serviceBulletins: stringToArray(this.cmmForm.controls['serviceBulletins'].value),
        incorporatedSeatAssemblies: stringToArray(this.cmmForm.controls['incorporatedSeatAssemblies'].value),
      });
      
      const formData = toFormData(this.cmmForm.value);

      const subs$ = this.cmmService.updateCMM(this.cmm.id, formData).subscribe(data => {
          console.log(data)
          this.cmmForm.reset();
          this.ui.spin$.next(false);
          this.snackBar.open(`CMM was Updated`,
            '',
            {
              duration: 2 * 1000
            });
        this.router.navigateByUrl('/modules/manuals/' + this.cmm.id + '&reload=true');
      },
        error => {
          console.log(error);
          this.ui.spin$.next(false);
        },
        () => {
          subs$.unsubscribe();
        });
    }
  }

}

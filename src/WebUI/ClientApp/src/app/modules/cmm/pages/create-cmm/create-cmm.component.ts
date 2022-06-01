import { Router } from '@angular/router';
import { CMMService } from './../../../../core/services/cmm.service';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { toFormData } from 'src/app/core/utils/utils';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UiService } from 'src/app/core/services/ui.service';
import { EntityService } from 'src/app/core/services/entity.service';
import { Subscription } from 'rxjs';
import { Entity } from 'src/app/core/interfaces/entity.interface';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-create-cmm',
  templateUrl: './create-cmm.component.html',
  styleUrls: ['./create-cmm.component.scss']
})
export class CreateCmmComponent implements OnInit, OnDestroy{
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
  tsoFormControl = new FormControl('', Validators.required);
  rolesFormControl = new FormControl('', Validators.required);
  entities: Entity[];
  entitySubs: Subscription;
  constructor(
    private cmmService: CMMService,
    private router: Router,
    private ui: UiService,
    private snackBar: MatSnackBar,
    private entityService: EntityService) {
      this.entitySubs = this.entityService.entities$.subscribe((data) => {
        console.log(data);
        if (data.length) {

          this.entities = data;
        }
      });
    }
    ngOnDestroy(): void {
      this.entitySubs.unsubscribe();
    }
  ngOnInit(): void {
    this.entityService.getEntities();
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
      incorporatedSeatAssemblies: event.target.value.split(/\n\r?/g)
    });
  }
  serviceBulletinsChange(event){
    this.cmmForm.patchValue({
      serviceBulletins: event.target.value.split(/\n\r?/g)
    });
  }
  referenceChange(event){
    this.cmmForm.patchValue({
      reference: event.target.value.split(/\n\r?/g)
    });
  }
  seatPartNumbersChange(event){
    this.cmmForm.patchValue({
      seatPartNumbers: event.target.value.split(/\n\r?/g)
    });
  }
  seatPartNumbersTSOChange(event){
    this.cmmForm.patchValue({
      seatPartNumbersTSO: event.target.value.split(/\n\r?/g)
    });
  }
  rolesChange(event){
    this.cmmForm.patchValue({
      roles: event.target.value.split(/\n\r?/g)
    });
  }
  onSubmit() {
    if (this.cmmForm.valid) {
      this.ui.spin$.next(true);
      const formData = toFormData(this.cmmForm.value);
      const subs$ = this.cmmService.createCMM(formData).subscribe(data => {
          console.log(data)
          this.cmmForm.reset();
          this.ui.spin$.next(false);
          this.snackBar.open(`CMM was Created`,
            '',
            {
              duration: 2 * 1000
            });
          this.router.navigateByUrl('/modules/manuals');
        },
        console.log,
        () => {
          subs$.unsubscribe();
        });
    }

  }

}

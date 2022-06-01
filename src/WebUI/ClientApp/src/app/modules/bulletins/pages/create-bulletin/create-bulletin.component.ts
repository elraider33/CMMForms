import { EntityService } from './../../../../core/services/entity.service';
import { Router } from '@angular/router';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { BulletinService } from 'src/app/core/services/bulletin.service';
import { toFormData } from 'src/app/core/utils/utils';
import { Subscription } from 'rxjs';
import { Entity } from 'src/app/core/interfaces/entity.interface';
import { CMMService } from 'src/app/core/services/cmm.service';
import { map } from 'rxjs/operators';
import { UiService } from 'src/app/core/services/ui.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-create-bulletin',
  templateUrl: './create-bulletin.component.html',
  styleUrls: ['./create-bulletin.component.scss']
})
export class CreateBulletinComponent implements OnInit, OnDestroy {
  bulletinForm = new FormGroup({
    requestedBy: new FormControl(),
    requestedOn: new FormControl(new Date(),Validators.required),
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
    cmmSubs: Subscription;
    cmmList = new FormControl();
    roles: string[] = [];
    rolesFormControl = new FormControl('', Validators.required);
    cmmFormControl = new FormControl('');
  constructor(
    private router: Router,
    private readonly bulletinService: BulletinService,
    private cmmService: CMMService,
    private entityService: EntityService,
    private snackBar: MatSnackBar,
    private ui : UiService,
    ) {
      this.entitySubs = this.entityService.entities$.subscribe((data) => {
        if (data.length) {
          this.entities = data;
        }
      });
      this.cmmSubs = this.cmmService.cmms$.pipe(
        map(cmms => {return cmms.map(c => c.cmmNumber)})
      ).subscribe(data => {
        if(data.length){
          console.log(data);
          this.cmms = data;
        }
      })

    }
  ngOnDestroy(): void {
    this.entitySubs.unsubscribe();
    this.cmmSubs.unsubscribe();
  }

  ngOnInit(): void {
    this.entityService.getEntities();
    this.cmmService.getCMMs();
  }

  onFileChange(event) {
      if (event.target.files.length > 0) {
        const file = event.target.files[0];
        this.bulletinForm.patchValue({
          file: file
        });
      }
  }
  seatPartChange(event){
    this.bulletinForm.patchValue({
      seatPartNumbers: event.target.value.split(/\n\r?/g)
    });
  }
  cmmChange(event){
    this.bulletinForm.patchValue({
      cmm: event.target.value.split(/\n\r?/g)
    });
  }
  rolesChange(event){
    this.bulletinForm.patchValue({
      roles: event.target.value.split(/\n\r?/g)
    });
  }
  addRole(){
    console.log(this.rolesFormControl.value)
    this.roles.push(this.rolesFormControl.value);
    console.log(this.roles);
    this.bulletinForm.patchValue({
      roles: this.roles
    });
    this.rolesFormControl.setValue('');
  }
  onSubmit(){
    if(this.bulletinForm.valid){
      this.ui.spin$.next(true);
      const formData = toFormData(this.bulletinForm.value);
      const subs$ = this.bulletinService.createBulletin(formData).subscribe(data => {
        console.log(data)
        this.bulletinForm.reset();
        this.ui.spin$.next(false);
        this.snackBar.open(`Service bulletin was Updated`, '', {
          duration: 2 * 1000
        });
        this.router.navigateByUrl('/modules/service-bulletins');
      },error => {
        console.log(error);
        this.ui.spin$.next(false);
      },
      () => {
        this.ui.spin$.next(false);
        subs$.unsubscribe();
      });
    }
  }
}

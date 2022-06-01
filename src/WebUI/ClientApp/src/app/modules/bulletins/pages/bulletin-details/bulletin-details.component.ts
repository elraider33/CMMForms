import { AuthService } from './../../../../core/services/auth.service';
import { finalize } from 'rxjs/operators';
import { Subscription } from 'rxjs';
import { BulletinService } from 'src/app/core/services/bulletin.service';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Bulletin } from 'src/app/core/interfaces/bulletin.interface';
import { FileService } from 'src/app/core/services/file.service';
import { HttpResponse } from '@angular/common/http';
import { downloadFile } from 'src/app/core/utils/utils';
import { UiService } from 'src/app/core/services/ui.service';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { DeleteDialogComponent } from 'src/app/shared/components/delete-dialog/delete-dialog.component';
import { MatSnackBar } from '@angular/material/snack-bar';
@Component({
  selector: 'app-bulletin-details',
  templateUrl: './bulletin-details.component.html',
  styleUrls: ['./bulletin-details.component.scss']
})
export class BulletinDetailsComponent implements OnInit, OnDestroy {
  subs: Subscription;
  bulletin: Bulletin = null;
  returnUrl: string;
  isAdmin: boolean;
  constructor(
    private readonly router: Router,
    private route: ActivatedRoute,
    private bulletinService: BulletinService,
    private ui: UiService,
    private authService: AuthService,
    public dialog: MatDialog,
    private snackBar: MatSnackBar,
    private fileService: FileService) {
      this.subs = this.bulletinService.bulletin$.subscribe(result => {
        if(result){
          this.bulletin = result;
          this.ui.spin$.next(false);
          console.log(result);
        }

      });

    }
  ngOnDestroy(): void {
    this.subs.unsubscribe();
  }
  getBooleanText(val){
    return val ? 'Yes' : 'No';
  }
  ngOnInit(): void {
    this.ui.spin$.next(true);
    this.isAdmin = this.authService.isAdmin();
    console.log(this.route.snapshot.paramMap);
    const params = this.route.snapshot.paramMap.get('id');
    let id = '';
    this.returnUrl = '/modules/service-bulletins';
    id = params;
    console.log(id);
    this.bulletinService.getBulletin(id);
  }
  formatStringArray(arr: string[]){
    return arr?.join("\n\r") || '';
  }

  deleteDialog(){
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      width: '350px',
      data: {type: 'Service Bulletin', id: this.bulletin.sbno}
    });
    dialogRef.afterClosed().subscribe(result => {
      if(result){
        this.ui.spin$.next(true);
        const subs = this.bulletinService.deleteBulletin(this.bulletin.id)
        .pipe(finalize(() => {
          subs.unsubscribe();
          this.ui.spin$.next(false);
          this.snackBar.open(`Service bulletin was deleted`, '', {
            duration: 2 * 1000
          });
          this.router.navigateByUrl('/modules/service-bulletins');
        }))
        .subscribe(data => {
          console.log(data)
        });
      }
      console.log('The dialog was closed');
      console.log(result);
    })
  }
  getDownloadUrl(id) {
    return this.fileService.downloadUrl(id);
  }
  downloadDocument(){
    this.ui.spin$.next(true);
    this.fileService.downloadDocument(this.bulletin.file?.filesId)
    .subscribe(data => {
      console.log(data);
      this.ui.spin$.next(false);
      downloadFile(data, this.bulletin.file?.filename);
    }, error => {
      this.ui.spin$.next(false);
      this.snackBar.open(`there was an error trying to get the file`, '', {
        duration: 2 * 1000
      });
      console.log('ERROR', error);
    });
  }
  onClose(){
    window.close();
  }
}

import { AuthService } from './../../../../core/services/auth.service';
import { CMMService } from './../../../../core/services/cmm.service';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router  } from '@angular/router';
import { Subscription } from 'rxjs';
import { CMM } from 'src/app/core/interfaces/cmm.interface';
import { downloadFile } from 'src/app/core/utils/utils';
import { FileService } from 'src/app/core/services/file.service';
import { UiService } from 'src/app/core/services/ui.service';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { finalize } from 'rxjs/operators';
import { DeleteDialogComponent } from 'src/app/shared/components/delete-dialog/delete-dialog.component';
@Component({
  selector: 'app-cmm-details',
  templateUrl: './cmm-details.component.html',
  styleUrls: ['./cmm-details.component.scss']
})
export class CmmDetailsComponent implements OnInit, OnDestroy {
  subs: Subscription;
  cmm: CMM = null;
  isAdmin: boolean;
  previousUrl: string;
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private authService: AuthService,
    private cmmService: CMMService,
    private ui: UiService,
    public dialog: MatDialog,
    private snackBar: MatSnackBar,
    private fileService: FileService) {
      this.subs = this.cmmService.cmm$.subscribe(result => {
        if(result){
          this.cmm = result;
          this.ui.spin$.next(false);
        }
      });


  }
  ngOnDestroy(): void {
    this.subs.unsubscribe();
  }

  ngOnInit(): void {
    this.ui.spin$.next(true);
    this.isAdmin = this.authService.isAdmin();
    let id = this.route.snapshot.paramMap.get('id');
    let edited = false;
    if (id.indexOf('reload')>0) {
      id = id.substring(0, id.indexOf('&reload'));
      edited = true;
    }
    
    if (edited) {
      window.opener.location.reload();
    }
    this.cmmService.getCMM(id);
  }
  getBooleanText(val){
    return val ? 'Yes' : 'No';
  }
  formatStringArray(arr: string[]){
    if(arr[0] === 'null') return '';
    return arr?.join("\n\r") || '';
  }

  deleteDialog(){
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      width: '350px',
      data: {type: 'CMM', id: this.cmm.cmmNumber}
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.ui.spin$.next(true);
        const subs = this.cmmService.deleteCmm(this.cmm.id)
          .pipe(finalize(() => {
            subs.unsubscribe();
            this.ui.spin$.next(false);
            this.snackBar.open(`CMM was deleted`,
              '',
              {
                duration: 2 * 1000
              });
            this.router.navigateByUrl('/modules/manuals');
          }))
          .subscribe(data => {
          });
      }
      console.log('The dialog was closed');
    });
  }
  getDownloadUrl(id) {
    return this.fileService.downloadUrl(id);
  }
  downloadDocument(){
    this.ui.spin$.next(true);
    this.fileService.downloadDocument(this.cmm.file?.filesId)
    .subscribe(data => {
      console.log(data);
      this.ui.spin$.next(false);
      downloadFile(data, this.cmm.file?.filename);
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

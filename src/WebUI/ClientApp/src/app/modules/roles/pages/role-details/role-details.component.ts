import { AuthService } from '../../../../core/services/auth.service';
import { CMMService } from '../../../../core/services/cmm.service';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { CMM } from 'src/app/core/interfaces/cmm.interface';
import { downloadFile } from 'src/app/core/utils/utils';
import { FileService } from 'src/app/core/services/file.service';
import { UiService } from 'src/app/core/services/ui.service';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { finalize } from 'rxjs/operators';
import { DeleteDialogComponent } from 'src/app/shared/components/delete-dialog/delete-dialog.component';
import Role from 'src/app/core/interfaces/Role.interface';
import { RoleService } from 'src/app/core/services/role.service';
@Component({
  selector: 'app-role-details',
  templateUrl: './role-details.component.html',
  styleUrls: ['./role-details.component.scss']
})
export class RoleDetailsComponent implements OnInit, OnDestroy {
  role: Role;
  isAdmin: boolean;
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private roleService: RoleService,
    private authService: AuthService,
    private ui: UiService,
    public dialog: MatDialog,
    private snackBar: MatSnackBar) {
    }
  ngOnDestroy(): void {
    // this.subs.unsubscribe();
  }

  ngOnInit(): void {
    this.ui.spin$.next(true);
    this.isAdmin = this.authService.isAdmin();
    const id = this.route.snapshot.paramMap.get('id');
    console.log(id);
    this.roleService.getById(id).subscribe(role => {
      if(role){
        this.role = role;
        this.ui.spin$.next(false);
      }
    });
  }

  deleteDialog(){
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      width: '350px',
      data: {type: 'Role', id: this.role.name}
    });
    dialogRef.afterClosed().subscribe(result => {
      if(result){
        this.ui.spin$.next(true);
        const subs = this.roleService.delete(this.role.id)
        .pipe(finalize(() => {
          subs.unsubscribe();
          this.ui.spin$.next(false);
          this.snackBar.open(`Role was deleted`, '', {
            duration: 2 * 1000
          });
          this.router.navigateByUrl('/modules/roles');
        }))
        .subscribe(data => {
          console.log(data)
        });
      }
      console.log('The dialog was closed');
      console.log(result);
    })
  }
}

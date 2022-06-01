import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import Role from 'src/app/core/interfaces/Role.interface';
import { RoleService } from 'src/app/core/services/role.service';
import { UiService } from 'src/app/core/services/ui.service';
@Component({
  selector: 'app-update-role',
  templateUrl: './update-role.component.html',
  styleUrls: ['./update-role.component.scss']
})
export class UpdateRoleComponent implements OnInit, OnDestroy {
  roleForm = new FormGroup({
    code: new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]),
    description: new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]),
  });
  role: Role;
  email: string[] = [];
  constructor(
    private roleService: RoleService,
    private router: Router,
    private route: ActivatedRoute,
    private ui: UiService,
    private snackBar: MatSnackBar
  ) {}
  ngOnDestroy(): void {
    // this.entitySubs.unsubscribe();
  }

  ngOnInit(): void {
    this.ui.spin$.next(true);
    const id = this.route.snapshot.paramMap.get("id");
    this.roleService.getById(id).subscribe(role => {
      this.roleForm.setValue({
        code: role.name,
        description: role.description??''
      });
      this.role = role;
      this.email = role.emailAddresses??[];
      this.ui.spin$.next(false);
    });
  }
  
  onAddEmailValue(event){
    this.email.push(event);
  }

  onSubmit() {
    if(this.roleForm.valid){
      this.ui.spin$.next(true);
      const role: Role = {
        name: this.roleForm.controls.code.value,
        description: this.roleForm.controls.description.value,
        emailAddresses: this.email,
        id: this.role.id
      }
      const subs$ = this.roleService.update(this.role.id, role).subscribe(
        (data) => {
          console.log(data);
          this.roleForm.reset();
          this.ui.spin$.next(false);
          this.snackBar.open(`Role was Updated`, "", {
            duration: 2 * 1000,
          });
          this.router.navigateByUrl("/modules/roles");
        },
        console.log,
        () => {
          subs$.unsubscribe();
        });

    }else{
      this.ui.spin$.next(false);
    }
  }

}

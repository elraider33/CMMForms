import { Router } from "@angular/router";
import { RoleService } from "../../../../core/services/role.service";
import { Component, OnDestroy, OnInit } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { UiService } from "src/app/core/services/ui.service";
import { MatSnackBar } from "@angular/material/snack-bar";
import Role from 'src/app/core/interfaces/Role.interface';

@Component({
  selector: "app-create-role",
  templateUrl: "./create-role.component.html",
  styleUrls: ["./create-role.component.scss"],
})
export class CreateRoleComponent implements OnInit, OnDestroy {
  roleForm = new FormGroup({
    code: new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]),
    description: new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]),
  });
  email: string[] = [];
  constructor(
    private roleService: RoleService,
    private router: Router,
    private ui: UiService,
    private snackBar: MatSnackBar
  ) {}
  ngOnDestroy(): void {}
  ngOnInit(): void {}

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
      }
      const subs$ = this.roleService.create(role).subscribe(
        (data) => {
          console.log(data);
          this.roleForm.reset();
          this.ui.spin$.next(false);
          this.snackBar.open(`Role was Created`, "", {
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

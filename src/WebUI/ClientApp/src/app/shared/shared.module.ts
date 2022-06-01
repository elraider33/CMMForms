import { DeleteDialogComponent } from './components/delete-dialog/delete-dialog.component';
import { AuthService } from 'src/app/core/services/auth.service';
import { TOKEN_KEY } from 'src/app/core/utils/constants';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import {MatTableModule} from '@angular/material/table';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatSidenavModule} from '@angular/material/sidenav';
import {MatListModule} from '@angular/material/list';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatPaginatorModule} from '@angular/material/paginator';
import {MatSortModule} from '@angular/material/sort';
import {CdkAccordionModule} from '@angular/cdk/accordion';
import { MenuListItemComponent } from './components/menu-list-item/menu-list-item.component';
import { MatIconModule } from "@angular/material/icon";
import { TopNavComponent } from './components/top-nav/top-nav.component';
import {MatButtonModule} from '@angular/material/button';
import {MatRadioModule} from '@angular/material/radio';
import {MatInputModule} from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatSelectModule } from '@angular/material/select';
// import { OAuthModule } from 'angular-oauth2-oidc';
import { JwtModule } from '@auth0/angular-jwt';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import {MatChipsModule} from '@angular/material/chips';
import {MatDialogModule} from '@angular/material/dialog';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import {MatExpansionModule} from '@angular/material/expansion';
import { InputMultipleComponent } from './components/input-multiple/input-multiple.component';
import { AutocompleteInputComponent } from './components/autocomplete-input/autocomplete-input.component';
@NgModule({
  declarations: [
    NavMenuComponent,
    MenuListItemComponent,
    TopNavComponent,
    DeleteDialogComponent,
    InputMultipleComponent,
    AutocompleteInputComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    RouterModule,
    MatTableModule,
    MatToolbarModule,
    MatSidenavModule,
    MatListModule,
    MatFormFieldModule,
    MatPaginatorModule,
    MatSortModule,
    CdkAccordionModule,
    MatIconModule,
    MatButtonModule,
    MatRadioModule,
    MatInputModule,
    MatCardModule,
    FlexLayoutModule,
    MatSelectModule,
    MatNativeDateModule,
    MatDatepickerModule,
    MatAutocompleteModule,
    MatChipsModule,
    MatDialogModule,
    MatSnackBarModule,
    MatExpansionModule
  ],
  exports:[
    CommonModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    RouterModule,
    MatTableModule,
    MatToolbarModule,
    MatSidenavModule,
    MatListModule,
    MatFormFieldModule,
    MatPaginatorModule,
    NavMenuComponent,
    MatSortModule,
    CdkAccordionModule,
    MatIconModule,
    MenuListItemComponent,
    TopNavComponent,
    MatButtonModule,
    MatRadioModule,
    MatInputModule,
    MatCardModule,
    FlexLayoutModule,
    MatSelectModule,
    MatNativeDateModule,
    MatDatepickerModule,
    MatAutocompleteModule,
    MatChipsModule,
    MatDialogModule,
    MatSnackBarModule,
    MatExpansionModule,
    InputMultipleComponent,
    AutocompleteInputComponent
  ]
})
export class SharedModule { }

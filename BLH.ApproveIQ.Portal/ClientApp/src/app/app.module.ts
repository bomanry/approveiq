import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

// PrimeNG modules
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';

// NgxMask module
import { NgxMaskModule } from 'ngx-mask';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/pages/home/home.component';
import { LoginComponent } from './components/pages/login/login.component';

// Shared components
import { PrimeNgInputComponent } from './components/shared/primeng-input/primeng-input.component';
import { EditHeaderComponent } from './components/shared/edit-header/edit-header.component';
import {ConfirmDialogModule} from "primeng/confirmdialog";
import {ConfirmationService} from "primeng/api";
import {HeaderComponent} from "./components/layout/header/header.component";
import {MenubarModule} from "primeng/menubar";


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    PrimeNgInputComponent,
    EditHeaderComponent,
    HeaderComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    InputTextModule,
    ButtonModule,
    CardModule,
    NgxMaskModule.forRoot(),
    AppRoutingModule,
    ConfirmDialogModule,
    MenubarModule
  ],
  providers: [ConfirmationService],
  bootstrap: [AppComponent]
})
export class AppModule { }

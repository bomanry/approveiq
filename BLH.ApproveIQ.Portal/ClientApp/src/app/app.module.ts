import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

// PrimeNG modules
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { TabViewModule } from 'primeng/tabview';
import { DropdownModule } from 'primeng/dropdown';
import { InputNumberModule } from 'primeng/inputnumber';
import { CalendarModule } from 'primeng/calendar';
import { TableModule } from 'primeng/table';
import { DialogModule } from 'primeng/dialog';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { ToastModule } from 'primeng/toast';
import { ConfirmDialogModule } from "primeng/confirmdialog";
import { MenubarModule } from "primeng/menubar";
import { CheckboxModule } from "primeng/checkbox";
import { RadioButtonModule } from "primeng/radiobutton";
import { TooltipModule } from "primeng/tooltip";
import { RippleModule } from "primeng/ripple";

// Services
import { ConfirmationService, MessageService } from "primeng/api";

// NgxMask module
import { NgxMaskModule } from 'ngx-mask';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/pages/home/home.component';
import { LoginComponent } from './components/pages/login/login.component';
import { InvoiceDetailComponent } from './components/pages/invoice-detail/invoice-detail.component';
import { InvoiceListComponent } from './components/pages/invoice-list/invoice-list.component';

// Shared components
import { PrimeNgInputComponent } from './components/shared/primeng-input/primeng-input.component';
import { EditHeaderComponent } from './components/shared/edit-header/edit-header.component';
import { HeaderComponent } from "./components/layout/header/header.component";
import { PrimeNgTableComponent } from './components/shared/primeng-table/primeng-table.component';

// Interceptors
import { AuthInterceptor } from './interceptors/auth.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    InvoiceDetailComponent,
    InvoiceListComponent,
    PrimeNgInputComponent,
    EditHeaderComponent,
    HeaderComponent,
    PrimeNgTableComponent
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
    TabViewModule,
    DropdownModule,
    InputNumberModule,
    CalendarModule,
    TableModule,
    DialogModule,
    ProgressSpinnerModule,
    ToastModule,
    ConfirmDialogModule,
    MenubarModule,
    CheckboxModule,
    RadioButtonModule,
    TooltipModule,
    RippleModule,
    NgxMaskModule.forRoot(),
    AppRoutingModule
  ],
  providers: [
    ConfirmationService,
    MessageService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

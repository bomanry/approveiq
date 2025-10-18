import {Component, Inject, inject, OnInit} from '@angular/core';
import { MenuItem } from 'primeng/api';
import { ComponentBase } from 'src/app/framework/models/componentBase';
import {BehaviorSubject, Observable} from "rxjs";
import {DynamicDialogRef} from "primeng/dynamicdialog";
import { AuthService } from 'src/app/framework/services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent extends ComponentBase implements OnInit {
  dialogRef: DynamicDialogRef | null = null;
  isLoggedIn = false;

  constructor(private authService: AuthService) {
    super();
  }

  items$: BehaviorSubject<MenuItem[]> = new BehaviorSubject<MenuItem[]>([]);
  logoutItem$: BehaviorSubject<MenuItem | null> = new BehaviorSubject<MenuItem | null>(null);

  items = this.items$.asObservable();
  logoutItem = this.logoutItem$.asObservable();

  ngOnInit(): void {
    // Subscribe to auth service login status changes
    this.authService.isLoggedIn$
      .pipe(this.takeUntilDestroy())
      .subscribe(isLoggedIn => {
        this.isLoggedIn = isLoggedIn;
        this.setLoginDisplay();
      });
  }

  setLoginDisplay() {
      // Navigation items (left side)
      let navigationItems: MenuItem[] = [
        {
          label: 'Dashboard',
          icon: 'pi pi-home',
          command: (() => {
            this.router.navigate(['/home']);
          })
        },
        {
          label: 'All Invoices',
          icon: 'pi pi-list',
          command: (() => {
            this.router.navigate(['/invoices']);
          })
        }
      ];

      // Logout item (right side)
      let logoutItem: MenuItem = {
        label: 'Logout',
        icon: 'pi pi-sign-out',
        command: (() => {
          this.authService.logout();
        })
      };

      if (this.isLoggedIn) {
        this.items$.next(navigationItems);
        this.logoutItem$.next(logoutItem);
      }
      else {
        this.items$.next([]);
        this.logoutItem$.next(null);
      }
  }

  goHome() {
    this.router.navigate(['/home']);
  }
}

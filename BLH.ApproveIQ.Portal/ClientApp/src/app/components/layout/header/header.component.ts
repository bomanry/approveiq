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

  items = this.items$.asObservable();

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
      //non admin routes
      let items: MenuItem[] = [
        {
          label: 'Logout',
          command: (() => {
            this.authService.logout();
          })
        },
      ];
      if (this.isLoggedIn) {
        this.items$.next(items);
      }
      else {
        this.items$.next([]);
      }
  }

  goHome() {
    this.router.navigate(['/home']);
  }
}

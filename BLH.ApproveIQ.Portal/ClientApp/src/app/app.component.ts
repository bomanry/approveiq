import { Component } from '@angular/core';
import { LoadingService } from './framework/services/loading.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'BLH ApproveIQ';

  constructor(public loadingService: LoadingService) {}
}

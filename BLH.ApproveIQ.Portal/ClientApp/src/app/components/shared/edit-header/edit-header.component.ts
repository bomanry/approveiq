import { Component, Input } from '@angular/core';

@Component({
  selector: 'edit-header',
  templateUrl: './edit-header.component.html',
  styleUrls: ['./edit-header.component.scss']
})
export class EditHeaderComponent {
  @Input() title: string = '';
  @Input() showBackButton: boolean = true;
  
  public back() {
    window.history.back();
  }
}

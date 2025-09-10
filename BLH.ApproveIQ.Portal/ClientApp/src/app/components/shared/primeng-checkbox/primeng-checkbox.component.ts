import { Component, Input } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'primeng-checkbox',
  templateUrl: './primeng-checkbox.component.html',
  styleUrls: ['./primeng-checkbox.component.scss']
})
export class PrimeNgCheckboxComponent {
  @Input() name: string = '';
  @Input() control: FormControl = new FormControl();

  @Input() label: string = '';

  @Input() binary: boolean = true;

  public Validators = Validators;
}

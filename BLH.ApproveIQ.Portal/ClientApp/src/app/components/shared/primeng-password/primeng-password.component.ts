import { Component, Input } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'primeng-password',
  templateUrl: './primeng-password.component.html',
  styleUrls: ['./primeng-password.component.scss']
})
export class PrimeNgPasswordComponent {
  @Input() name: string = '';
  @Input() control: FormControl = new FormControl();

  @Input() label: string = '';

  @Input() feedback: boolean = false;
  @Input() autocomplete: string = '';

  public Validators = Validators;
}

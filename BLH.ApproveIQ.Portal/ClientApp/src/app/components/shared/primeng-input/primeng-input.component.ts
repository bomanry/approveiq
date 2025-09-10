import { Component, Input, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'primeng-input',
  templateUrl: './primeng-input.component.html',
  styleUrls: ['./primeng-input.component.scss']
})
export class PrimeNgInputComponent implements OnInit {
  @Input() name: string = '';
  @Input() control: FormControl = new FormControl();

  @Input() label: string = '';
  @Input() type: string = '';

  @Input() autocomplete: string = '';
  @Input() mask: string = '';
  @Input() prefix: string = '';
  @Input() thousandSeparator: string = '';
  @Input() allowNegativeNumbers: boolean = false;

  @Input() currency: '' | 'USD' = '';

  public Validators = Validators;

  ngOnInit(): void {
    if (this.currency == 'USD') {
      this.mask = "separator.2";
      this.prefix = "$";
      this.thousandSeparator = ",";
      this.allowNegativeNumbers = true;
    }
  }

}

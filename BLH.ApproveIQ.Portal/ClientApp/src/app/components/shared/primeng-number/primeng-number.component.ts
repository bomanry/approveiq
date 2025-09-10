import { Component, Input, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'primeng-number',
  templateUrl: './primeng-number.component.html',
  styleUrls: ['./primeng-number.component.scss']
})
export class PrimeNgNumberComponent implements OnInit {
  @Input() name: string = '';
  @Input() control: FormControl = new FormControl();

  @Input() label: string = '';
  @Input() type: string = '';

  @Input() inputId: string;
  @Input() min: number;
  @Input() max: number;
  @Input() mode: string;
  @Input() maxFractionDigits: number = 0;

  @Input() showButtons: boolean = true;
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

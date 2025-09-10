import { Component, Input } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'primeng-multiselect',
  templateUrl: './primeng-multiselect.component.html',
  styleUrls: ['./primeng-multiselect.component.scss']
})
export class PrimeNgMultiSelectComponent {
  @Input() name: string = '';
  @Input() control: FormControl = new FormControl();

  @Input() label: string = '';

  @Input() options: any[] = [];
  @Input() showClear: boolean = false;

  @Input() filter: boolean = false;
  @Input() filterBy: string = 'value';

  @Input() placeholder: string = '';

  @Input() optionLabel: string = 'label';
  @Input() optionValue: string = 'value';

  @Input() disabled:boolean = false;

  public Validators = Validators;

  public isStringArray(arr: any[]): arr is string[] {
    return arr.every(item => typeof item === 'string');
  }
}

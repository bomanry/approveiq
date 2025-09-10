import { Component, Input, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'primeng-textarea',
  templateUrl: './primeng-textarea.component.html',
  styleUrls: ['./primeng-textarea.component.scss']
})
export class PrimeNgTextAreaComponent {
  @Input() name: string = '';
  @Input() control: FormControl = new FormControl();

  @Input() label: string = '';
  @Input() rows: number = 5;

  @Input() disabled: boolean = false;

  public Validators = Validators;
}

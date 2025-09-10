import { Component, EventEmitter, Input, Output } from '@angular/core';
import { LazyLoadEvent } from 'primeng/api';
import { Table } from 'primeng/table';
import PrimeNgTableColumn from 'src/app/framework/models/primeNgTableColumn';
import { nameOf } from 'src/app/framework/utilities/nameof.utility';
import {SelectableDto} from "../../../framework/models/selectableDto";
import {FormControl, Validators} from "@angular/forms";
import { OverlayModule } from 'primeng/overlay';

@Component({
  selector: 'primeng-datepicker',
  templateUrl: './primeng-datepicker.component.html',
  styleUrls: ['./primeng-datepicker.component.scss']
})
export class PrimeNgDatepickerComponent {

  @Input() name: string = '';
  @Input() control: FormControl = new FormControl();

  @Input() label: string = '';
  @Input() showTime: boolean = false;
  @Input() timeOnly: boolean = false;

  @Input() public disabledDays: number[] = [];
  @Input() public stepMinute: number = 1;
  
  public Validators = Validators;
}

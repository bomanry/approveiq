import { Component, EventEmitter, Input, Output } from '@angular/core';
import { LazyLoadEvent } from 'primeng/api';
import { Table } from 'primeng/table';
import PrimeNgTableColumn from 'src/app/framework/models/primeNgTableColumn';
import { nameOf } from 'src/app/framework/utilities/nameof.utility';
import {SelectableDto} from "../../../framework/models/selectableDto";
import {FormControl, Validators} from "@angular/forms";

@Component({
  selector: 'primeng-rating',
  templateUrl: './primeng-rating.component.html',
  styleUrls: ['./primeng-rating.component.scss']
})
export class PrimeNgRatingComponent {

  @Input() name: string = '';
  @Input() control: FormControl = new FormControl();

  @Input() label: string = '';
  @Input() showTime: boolean = false;
  @Input() disabled: boolean = false;

  public Validators = Validators;
}

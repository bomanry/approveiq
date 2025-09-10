import {
  Component,
  ElementRef,
  EventEmitter,
  Input,
  Output,
} from '@angular/core';
import { LazyLoadEvent } from 'primeng/api';
import { Table, TableModule } from 'primeng/table';
import PrimeNgTableColumn from 'src/app/framework/models/primeNgTableColumn';
import { nameOf } from 'src/app/framework/utilities/nameof.utility';
import {SelectableDto} from "../../../framework/models/selectableDto";
import {Router} from "@angular/router";
import {getItemWithExpiry, setItemWithExpiry} from "../../../framework/utilities/localstorage.utility";

@Component({
  selector: 'primeng-table',
  templateUrl: './primeng-table.component.html',
  styleUrls: ['./primeng-table.component.scss']
})
export class PrimeNgTableComponent {
  constructor(private router: Router, private el: ElementRef) { }

  public static LOCAL_STORAGE_FILTER_KEY = "FILTERKEY-";

  isFirstLoad = true;

  @Input() public dataList: any[] = [];

  @Input() public lazy: boolean = true;
  @Input() public paginator: boolean = true;

  @Input() public loading: boolean = false;
  @Input() public totalRecords: number = 0;

  @Input() public sortField: string;
  @Input() public sortOrder: number;

  @Output() onLazyLoad: EventEmitter<LazyLoadEvent> = new EventEmitter<LazyLoadEvent>();
  @Output() onAddClicked: EventEmitter<void> = new EventEmitter<void>();
  @Output() onEditClicked: EventEmitter<any> = new EventEmitter<any>();
  @Output() onDeleteClicked: EventEmitter<any> = new EventEmitter<any>();

  @Input() public globalFilterFields: string[] = [];
  public searchBoxContent = "";

  /*
    public cols: PrimeNgTableColumn[]  = [
      { field: nameOf((_: ExampleDto) => _.data)!, header: 'Data', filterType: 'text', customExportHeader: 'Example Data' },
      { field: '', header: '', sortable: false, type: 'Action', action: (example: ExampleDto) => { this.editExample(example); }, icon: 'pi-pencil' }
    ];
  */
  @Input() public cols: PrimeNgTableColumn[];

  /*
    public actionLinks: any[] = [
      { label: 'Example Action Link', tooltip: 'Example',  icon: 'pi-pencil', action: () => { this.doSomething(); } }
    ];
  */
  @Input() public actionLinks: any[];

  public isSelectable<T>(obj: any): obj is SelectableDto {
    return (
      obj &&
      typeof obj.checkboxStatus === 'boolean'
    );
  }

  @Input() public customExportHeader: string = '';

  @Input() public hasCsvExport: boolean = false;

  @Input() public hasClearFilters: boolean = true;

  @Input() public hasSearchBox: boolean = true;
  @Input() public searchBoxOnRight: boolean = true;

  @Input() public hasAddAction: boolean = true;
  @Input() public addActionLabel: string = '';

  @Input() public hasEditButton: boolean = true;
  @Input() public editButtonLabel: string = 'Edit';

  @Input() public customEditIcon: string;

  @Input() public hasDeleteButton: boolean = true;
  @Input() public deleteButtonLabel: string = 'Delete';
  @Input() public disableDeleteCondition: Function;

  @Input() public tableButtonClass: string = '';

  @Input() public first: number = 0;
  @Input() public rows: number = 10;
  @Input() public rowsPerPageOptions: number[] = [10,25,50];

  appendId(basePath: string, id: string) {
    // Ensure the basePath ends with a single slash
    if (!basePath.endsWith('/')) {
      basePath += '/';
    }
    return `${basePath}${id}`;
  }

  public checkLinkAndCallIfPossible(col: PrimeNgTableColumn, idField: string) {
    if(idField)
      this.router.navigate([this.appendId(col.link!, idField)]);
    else
      this.router.navigate([col.link]);
  }

  public get customExportHeaderField(): string {
    return nameOf((_: PrimeNgTableColumn) => _.customExportHeader)!;
  }

  clear(table: Table) {
    table.clear();
    this.searchBoxContent = "";
  }

  filter(table: Table, event: any) {
    table.filterGlobal(event.target.value, 'contains')
  }

  public emitOnLazyLoad(event: any, table: Table) {
    let parent = this.el.nativeElement.parentElement;

    if(this.isFirstLoad) {
      let localEvent = getItemWithExpiry(PrimeNgTableComponent.LOCAL_STORAGE_FILTER_KEY + parent.tagName);

      if(localEvent) {
        event = JSON.parse(localEvent);
        this.first = event["first"] ?? 0;
        table.filters = event["filters"] ?? {};
        table.sortField = event["sortField"] ?? {};
        table.sortOrder = event["sortOrder"] ?? {};
      }
    }

    this.isFirstLoad = false;
    setItemWithExpiry(PrimeNgTableComponent.LOCAL_STORAGE_FILTER_KEY + parent.tagName, JSON.stringify(event), 20);
    this.onLazyLoad.emit(event);
  }



}

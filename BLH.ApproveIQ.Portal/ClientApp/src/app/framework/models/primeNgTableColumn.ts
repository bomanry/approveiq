export default class PrimeNgTableColumn {
    header: string = '';
    field: string = '';
    style?: string = '';
    sortable?: boolean = true;
    filterType?: '' | 'text' | 'date' | 'numeric' | 'boolean' = '';
    customExportHeader?: string = '';
    type?: 'Mask' | 'Date' | 'UTCDate' | 'Boolean' | 'Inverted Boolean' | 'Checkmark Only' | 'Action' | 'Currency' | 'Checkbox' | 'Scaled Value' | 'GradeLevel' | 'Unenroll' | 'ApprovalButton'
    maxScale?: number = 0;
    buttonClass?: string = '';
    dateFormat?: string = '';
    maskFormat?: string = '';
    currencyFormat?: string = '';
    action?: (...args: any[]) => any;
    icon?: string;
    label?: string;
    condition?: (...args: any[]) => boolean;
    checkboxStatus?: boolean = false;
    link?: string = '';
    linkIdField?: string = '';
}

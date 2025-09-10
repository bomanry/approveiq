import { AuditableEntity } from '../../../b-l-h/primitives/auditable-entity';
import { Invoice } from './invoice';

export class InvoiceItem extends AuditableEntity {
    invoiceId: string;
    businessUnit: string;
    buDescription: string;
    costCode: string;
    costCodeDesc: string;
    costType: string;
    costTypeDesc: string;
    orderNumber: string;
    line: string;
    orderSuffix: string;
    glLineType: string;
    qty?: number;
    uom: string;
    unitPrice?: number;
    amount?: number;
    invoice = new Invoice();
}

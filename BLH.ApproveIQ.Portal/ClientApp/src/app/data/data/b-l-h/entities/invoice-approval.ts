import { AuditableEntity } from '../../../b-l-h/primitives/auditable-entity';
import { Invoice } from './invoice';
import { User } from './user';

export class InvoiceApproval extends AuditableEntity {
    invoiceId: string;
    fromUserId: string;
    toUserId: string;
    fromStatus: string;
    toStatus: string;
    comments: string;
    action: string;
    invoice = new Invoice();
    fromUser = new User();
    toUser = new User();
}

import { AggregateRoot } from './aggregate-root';

export class AuditableEntity extends AggregateRoot {
    createdUserId: string;
    createdOnUtc = new Date();
    modifiedUserId?: string;
    modifiedOnUtc = new Date();
}

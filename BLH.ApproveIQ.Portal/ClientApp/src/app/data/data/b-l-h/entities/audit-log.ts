import { Entity } from '../../../b-l-h/primitives/entity';

export class AuditLog extends Entity {
    action: string;
    entityId: string;
    entityName: string;
    oldValues: string;
    newValues: string;
    affectedColumns: string;
    createdUserId: string;
    createdOnUtc = new Date();
}

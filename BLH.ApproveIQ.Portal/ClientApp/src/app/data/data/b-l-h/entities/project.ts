import { AuditableEntity } from '../../../b-l-h/primitives/auditable-entity';

export class Project extends AuditableEntity {
    name: string;
    buJobNumber: string;
}

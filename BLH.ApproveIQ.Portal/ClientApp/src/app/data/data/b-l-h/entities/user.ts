import { AuditableEntity } from '../../../b-l-h/primitives/auditable-entity';

export class User extends AuditableEntity {
    firstName: string;
    lastName: string;
    email: string;
}

import { AuditableEntity } from '../../../b-l-h/primitives/auditable-entity';

export class B2CUser extends AuditableEntity {
    displayName: string;
    userPrincipalName: string;
    accountEnabled?: boolean;
    role: string;
}

import {AuditableEntity} from "../../../b-l-h/primitives/auditable-entity";

export interface UserDto extends AuditableEntity {
  firstName: string;
  lastName: string;
  email: string;
}
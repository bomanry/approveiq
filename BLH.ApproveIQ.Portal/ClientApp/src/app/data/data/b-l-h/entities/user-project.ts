import { AuditableEntity } from '../../../b-l-h/primitives/auditable-entity';
import { User } from './user';
import { Project } from './project';

export class UserProject extends AuditableEntity {
    userId: string;
    projectId: string;
    projectRole: string;
    user = new User();
    project = new Project();
}

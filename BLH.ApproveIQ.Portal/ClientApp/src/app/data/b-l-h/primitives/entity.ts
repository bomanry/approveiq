import { Equatable } from '../../equatable';

export abstract class Entity implements Equatable<Entity> {
    id: string;
    isActive: boolean;
    isDeleted: boolean;
}

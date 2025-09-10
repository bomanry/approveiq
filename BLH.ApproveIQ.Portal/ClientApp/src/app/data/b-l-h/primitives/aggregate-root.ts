import { Entity } from './entity';
import { Eventable } from './eventable';

export abstract class AggregateRoot extends Entity implements Eventable {
}

import { Entity } from '../../../b-l-h/primitives/entity';

export class ExceptionLog extends Entity {
    createdDate = new Date();
    message: string;
    innerExceptionStackTrace: string;
    innerExceptionMessage: string;
    stackTrace: string;
}

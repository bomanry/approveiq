export class AuditLogDto {
    id?: string;
    action: string;
    entityId: string;
    entityName: string;
    oldValues: string;
    newValues: string;
    affectedColumns: string;
    createdUserId: string;
    createdUserName: string;
    createdOnUtc = new Date();
}

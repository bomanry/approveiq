using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using BLH.ApproveIQ.Domain.Constants;
using BLH.ApproveIQ.Domain.Entities;
using BLH.ApproveIQ.Domain.Identity.Models;
using BLH.ApproveIQ.Domain.Models.Audit;
using BLH.ApproveIQ.Domain.Primitives;
using BLH.ApproveIQ.Domain.Services;

namespace BLH.ApproveIQ.Persistence.Interceptors;
public sealed class UpdateAuditableEntitiesInterceptor
    : SaveChangesInterceptor
{
    private readonly ICurrentUserService _currentUserService;
    public UpdateAuditableEntitiesInterceptor(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        DbContext? dbContext = eventData.Context;

        if (dbContext is null)
        {
            return base.SavingChangesAsync(
                eventData,
                result,
                cancellationToken);
        }
        if (_currentUserService.UserExists)
        {
            OnBeforeSaveChanges(_currentUserService.UserId!.Value, dbContext);
        }

        IEnumerable<EntityEntry<AuditableEntity>> entries =
            dbContext
                .ChangeTracker
                .Entries<AuditableEntity>();

        foreach (EntityEntry<AuditableEntity> entityEntry in entries)
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(a => a.CreatedOnUtc).CurrentValue = DateTime.UtcNow;
                entityEntry.Property(a => a.CreatedUserId).CurrentValue = _currentUserService.UserId!.Value;
                entityEntry.Property(a => a.ModifiedOnUtc).CurrentValue = DateTime.UtcNow;
                entityEntry.Property(a => a.ModifiedUserId).CurrentValue = _currentUserService.UserId!.Value;
            }

            if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(a => a.ModifiedOnUtc).CurrentValue = DateTime.UtcNow;
                entityEntry.Property(a => a.ModifiedUserId).CurrentValue = _currentUserService.UserId!.Value;
            }
        }

        return base.SavingChangesAsync(
            eventData,
            result,
            cancellationToken);
    }

    private void OnBeforeSaveChanges(Guid userId, DbContext dbContext)
    {
        IEnumerable<EntityEntry<AuditableEntity>> entries =
          dbContext
              .ChangeTracker
              .Entries<AuditableEntity>();

        var auditEntries = new List<AuditLogEntry>();

        foreach (var entry in entries)
        {
            if (entry.Entity is AuditLog || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                continue;

            var auditEntry = new AuditLogEntry(entry);
            auditEntry.EntityName = entry.Entity.GetType().Name;
            auditEntry.UserId = userId;
            auditEntries.Add(auditEntry);
            foreach (var property in entry.Properties)
            {
                string propertyName = property.Metadata.Name;
                switch (propertyName)
                {
                    case nameof(AuditableEntity.CreatedUserId):
                        continue;
                    case nameof(AuditableEntity.CreatedOnUtc):
                        continue;
                    case nameof(AuditableEntity.ModifiedUserId):
                        continue;
                    case nameof(AuditableEntity.ModifiedOnUtc):
                        continue;
                }
                if (property.Metadata.IsPrimaryKey())
                {
                    auditEntry.EntityId = new Guid(property.CurrentValue.ToString());
                    continue;
                }
                switch (entry.State)
                {
                    case EntityState.Added:
                        auditEntry.AuditLogAction = AuditLogAction.Create;
                        auditEntry.NewValues[propertyName] = property.CurrentValue;
                        break;
                    case EntityState.Deleted:
                        auditEntry.AuditLogAction = AuditLogAction.Delete;
                        auditEntry.OldValues[propertyName] = property.OriginalValue;
                        break;
                    case EntityState.Modified:
                        if (property.IsModified)
                        {
                            auditEntry.ChangedColumns.Add(propertyName);
                            auditEntry.AuditLogAction = AuditLogAction.Update;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                        }
                        break;
                }
            }
        }
        foreach (var auditEntry in auditEntries)
        {
            dbContext.Set<AuditLog>().AddAsync(auditEntry.ToAudit());
        }
    }
}

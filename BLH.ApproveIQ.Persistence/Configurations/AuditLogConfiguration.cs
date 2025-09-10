using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BLH.ApproveIQ.Domain.Entities;
using BLH.ApproveIQ.Persistence.Constants;

namespace BLH.ApproveIQ.Persistence.Configurations;

internal sealed class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.ToTable(TableNames.AuditLogs);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CreatedUserId).HasColumnType("uniqueidentifier");
        builder.Property(x => x.CreatedUserId).IsRequired();
        builder.Property(x => x.EntityId).HasColumnType("uniqueidentifier");
        builder.Property(x => x.EntityId).IsRequired();
        builder.Property(x => x.Action).HasColumnType("varchar");
        builder.Property(x => x.Action).HasMaxLength(25);
        builder.Property(x => x.EntityName).HasColumnType("varchar");
        builder.Property(x => x.EntityName).HasMaxLength(100);
        builder.Property(x => x.EntityName).IsRequired();
        builder.Property(x => x.OldValues).HasColumnType("varchar(MAX)");
        builder.Property(x => x.NewValues).HasColumnType("varchar(MAX)");
        builder.Property(x => x.AffectedColumns).HasColumnType("varchar(MAX)");
    }
}

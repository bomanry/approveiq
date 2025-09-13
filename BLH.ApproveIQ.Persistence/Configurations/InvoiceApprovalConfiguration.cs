using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BLH.ApproveIQ.Domain.Entities;
using BLH.ApproveIQ.Persistence.Constants;

namespace BLH.ApproveIQ.Persistence.Configurations;

internal sealed class InvoiceApprovalConfiguration : IEntityTypeConfiguration<InvoiceApproval>
{
    public void Configure(EntityTypeBuilder<InvoiceApproval> builder)
    {
        builder.ToTable(TableNames.InvoiceApprovals);

        builder.HasKey(x => x.Id);

        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.Property(x => x.InvoiceId)
            .IsRequired();

        builder.Property(x => x.FromUserId)
            .IsRequired();

        builder.Property(x => x.ToUserId)
            .IsRequired();

        builder.Property(x => x.FromStatus)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.ToStatus)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Action)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Comments)
            .HasMaxLength(1000)
            .IsRequired(false);

        // Foreign key relationships
        builder.HasOne(x => x.Invoice)
            .WithMany()
            .HasForeignKey(x => x.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.FromUser)
            .WithMany()
            .HasForeignKey(x => x.FromUserId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent deleting users with approval history

        builder.HasOne(x => x.ToUser)
            .WithMany()
            .HasForeignKey(x => x.ToUserId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent deleting users with approval history

        // Index for querying approvals by invoice
        builder.HasIndex(x => x.InvoiceId);

        // Index for querying pending items for a user
        builder.HasIndex(x => x.ToUserId);
    }
}

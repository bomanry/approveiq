using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BLH.ApproveIQ.Domain.Entities;
using BLH.ApproveIQ.Persistence.Constants;

namespace BLH.ApproveIQ.Persistence.Configurations;

internal sealed class InvoiceItemConfiguration : IEntityTypeConfiguration<InvoiceItem>
{
    public void Configure(EntityTypeBuilder<InvoiceItem> builder)
    {
        builder.ToTable(TableNames.InvoiceItems);

        builder.HasKey(x => x.Id);

        builder.HasQueryFilter(x => !x.IsDeleted);

        // Foreign Key
        builder.Property(x => x.InvoiceId).IsRequired();

        // Invoice Line properties
        builder.Property(x => x.BusinessUnit).HasMaxLength(50);
        builder.Property(x => x.BuDescription).HasMaxLength(200);
        builder.Property(x => x.CostCode).HasMaxLength(50);
        builder.Property(x => x.CostCodeDesc).HasMaxLength(200);
        builder.Property(x => x.CostType).HasMaxLength(50);
        builder.Property(x => x.CostTypeDesc).HasMaxLength(200);
        builder.Property(x => x.OrderNumber).HasMaxLength(100);
        builder.Property(x => x.Line).HasMaxLength(20);
        builder.Property(x => x.OrderSuffix).HasMaxLength(20);
        builder.Property(x => x.GlLineType).HasMaxLength(50);
        builder.Property(x => x.Uom).HasMaxLength(20);

        // Decimal properties with precision
        builder.Property(x => x.Qty).HasColumnType("decimal(18,4)");
        builder.Property(x => x.UnitPrice).HasColumnType("decimal(18,4)");
        builder.Property(x => x.Amount).HasColumnType("decimal(18,2)");

        // Navigation property relationship already configured in InvoiceConfiguration
        builder.HasOne(x => x.Invoice)
            .WithMany(x => x.Lines)
            .HasForeignKey(x => x.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);

        // Index for performance
        builder.HasIndex(x => x.InvoiceId);
    }
}
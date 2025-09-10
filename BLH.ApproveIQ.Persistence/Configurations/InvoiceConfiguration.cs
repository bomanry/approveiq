using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BLH.ApproveIQ.Domain.Entities;
using BLH.ApproveIQ.Persistence.Constants;

namespace BLH.ApproveIQ.Persistence.Configurations;

internal sealed class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable(TableNames.Invoices);

        builder.HasKey(x => x.Id);

        builder.HasQueryFilter(x => !x.IsDeleted);

        // Invoice Info properties
        builder.Property(x => x.ApInvoiceType).HasMaxLength(50);
        builder.Property(x => x.InvoiceStatus).HasMaxLength(50);
        builder.Property(x => x.LegacyOrderNumber).HasMaxLength(100);
        builder.Property(x => x.BuJobNumber).HasMaxLength(100);
        builder.Property(x => x.OrderType).HasMaxLength(50);
        builder.Property(x => x.Category).HasMaxLength(100);

        // Company / JDE properties
        builder.Property(x => x.Company).HasMaxLength(100);
        builder.Property(x => x.JdeOrderNumber).HasMaxLength(100);

        // Vendor Info properties
        builder.Property(x => x.VendorId).HasMaxLength(50);
        builder.Property(x => x.VendorName).HasMaxLength(200);
        builder.Property(x => x.VendorType).HasMaxLength(50);
        builder.Property(x => x.VendorStreetAddress).HasMaxLength(255);
        builder.Property(x => x.VendorCity).HasMaxLength(100);
        builder.Property(x => x.VendorState).HasMaxLength(50);
        builder.Property(x => x.VendorZip).HasMaxLength(20);

        // Amount Info properties with precision
        builder.Property(x => x.NetAmount).HasColumnType("decimal(18,2)");
        builder.Property(x => x.MiscAmount).HasColumnType("decimal(18,2)");
        builder.Property(x => x.FreightAmount).HasColumnType("decimal(18,2)");
        builder.Property(x => x.GrossAmount).HasColumnType("decimal(18,2)");
        builder.Property(x => x.TaxableAmount).HasColumnType("decimal(18,2)");
        builder.Property(x => x.TaxAmount).HasColumnType("decimal(18,2)");
        builder.Property(x => x.RetainagePct).HasColumnType("decimal(5,2)");
        builder.Property(x => x.RetainageAmount).HasColumnType("decimal(18,2)");
        builder.Property(x => x.AmountToPay).HasColumnType("decimal(18,2)");

        builder.Property(x => x.PaymentTerms).HasMaxLength(100);
        builder.Property(x => x.TaxExCode).HasMaxLength(20);
        builder.Property(x => x.TaxArea).HasMaxLength(50);
        builder.Property(x => x.Currency).HasMaxLength(10);

        // Processing Info properties
        builder.Property(x => x.InvoiceDescription).HasMaxLength(500);
        builder.Property(x => x.VoucherNumber).HasMaxLength(50);
        builder.Property(x => x.CheckNumber).HasMaxLength(50);

        // Navigation properties
        builder.HasMany(x => x.Lines)
            .WithOne(x => x.Invoice)
            .HasForeignKey(x => x.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
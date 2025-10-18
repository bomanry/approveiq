using BLH.ApproveIQ.Application.Abstractions.Messaging;
using BLH.ApproveIQ.Domain.Entities;
using BLH.ApproveIQ.Domain.Repositories;
using BLH.ApproveIQ.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace BLH.ApproveIQ.Application.Invoices.Commands;

public sealed record UpdateInvoiceCommand(
    Guid Id,
    string? InvoiceNumber,
    string? ApInvoiceType,
    string? InvoiceStatus,
    Guid? CurrentlyAssignedToUserId,
    string? JdeOrderNumber,
    string? LegacyOrderNumber,
    string? OrderType,
    string? BuJobNumber,
    string? Category,
    string? Company,
    string? VendorId,
    string? VendorName,
    string? VendorType,
    string? VendorStreetAddress,
    string? VendorCity,
    string? VendorState,
    string? VendorZip,
    decimal? NetAmount,
    decimal? MiscAmount,
    decimal? FreightAmount,
    decimal? GrossAmount,
    decimal? TaxableAmount,
    string? TaxExCode,
    string? TaxArea,
    decimal? TaxAmount,
    decimal? RetainagePct,
    decimal? RetainageAmount,
    decimal? AmountToPay,
    string? Currency,
    string? PaymentTerms,
    bool? PaymentHoldFlag,
    DateTime? GlDate,
    DateTime? InvoiceDate,
    DateTime? ReceivedDate,
    string? InvoiceDescription,
    string? VoucherNumber,
    string? CheckNumber,
    DateTime? CheckDate) : ICommand<Invoice>;

internal sealed class UpdateInvoiceCommandHandler(
    IGenericRepository<Invoice> invoiceRepository,
    IUnitOfWork unitOfWork) 
    : ICommandHandler<UpdateInvoiceCommand, Invoice>
{
    public async Task<Result<Invoice>> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
    {
        var invoice = await invoiceRepository.GetQueryable()
            .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

        if (invoice is null)
        {
            return Result.Failure<Invoice>(new Error("Invoice.NotFound", "Invoice not found"));
        }

        // Update invoice properties
        if (!string.IsNullOrEmpty(request.InvoiceNumber))
            invoice.InvoiceNumber = request.InvoiceNumber;
        
        if (request.ApInvoiceType is not null)
            invoice.ApInvoiceType = request.ApInvoiceType;
        
        if (request.InvoiceStatus is not null)
            invoice.InvoiceStatus = request.InvoiceStatus;
        
        if (request.CurrentlyAssignedToUserId.HasValue)
            invoice.CurrentlyAssignedToUserId = request.CurrentlyAssignedToUserId.Value;
        
        if (request.JdeOrderNumber is not null)
            invoice.JdeOrderNumber = request.JdeOrderNumber;
        
        if (request.LegacyOrderNumber is not null)
            invoice.LegacyOrderNumber = request.LegacyOrderNumber;
        
        if (request.OrderType is not null)
            invoice.OrderType = request.OrderType;
        
        if (request.BuJobNumber is not null)
            invoice.BuJobNumber = request.BuJobNumber;
        
        if (request.Category is not null)
            invoice.Category = request.Category;
        
        if (request.Company is not null)
            invoice.Company = request.Company;

        // Vendor Information
        if (request.VendorId is not null)
            invoice.VendorId = request.VendorId;
        
        if (request.VendorName is not null)
            invoice.VendorName = request.VendorName;
        
        if (request.VendorType is not null)
            invoice.VendorType = request.VendorType;
        
        if (request.VendorStreetAddress is not null)
            invoice.VendorStreetAddress = request.VendorStreetAddress;
        
        if (request.VendorCity is not null)
            invoice.VendorCity = request.VendorCity;
        
        if (request.VendorState is not null)
            invoice.VendorState = request.VendorState;
        
        if (request.VendorZip is not null)
            invoice.VendorZip = request.VendorZip;

        // Amount Information
        if (request.NetAmount.HasValue)
            invoice.NetAmount = request.NetAmount.Value;
        
        if (request.MiscAmount.HasValue)
            invoice.MiscAmount = request.MiscAmount.Value;
        
        if (request.FreightAmount.HasValue)
            invoice.FreightAmount = request.FreightAmount.Value;
        
        if (request.GrossAmount.HasValue)
            invoice.GrossAmount = request.GrossAmount.Value;
        
        if (request.TaxableAmount.HasValue)
            invoice.TaxableAmount = request.TaxableAmount.Value;
        
        if (request.TaxExCode is not null)
            invoice.TaxExCode = request.TaxExCode;
        
        if (request.TaxArea is not null)
            invoice.TaxArea = request.TaxArea;
        
        if (request.TaxAmount.HasValue)
            invoice.TaxAmount = request.TaxAmount.Value;
        
        if (request.RetainagePct.HasValue)
            invoice.RetainagePct = request.RetainagePct.Value;
        
        if (request.RetainageAmount.HasValue)
            invoice.RetainageAmount = request.RetainageAmount.Value;
        
        if (request.AmountToPay.HasValue)
            invoice.AmountToPay = request.AmountToPay.Value;
        
        if (request.Currency is not null)
            invoice.Currency = request.Currency;
        
        if (request.PaymentTerms is not null)
            invoice.PaymentTerms = request.PaymentTerms;
        
        if (request.PaymentHoldFlag.HasValue)
            invoice.PaymentHoldFlag = request.PaymentHoldFlag.Value;

        // Date Information
        if (request.GlDate.HasValue)
            invoice.GlDate = request.GlDate.Value;
        
        if (request.InvoiceDate.HasValue)
            invoice.InvoiceDate = request.InvoiceDate.Value;
        
        if (request.ReceivedDate.HasValue)
            invoice.ReceivedDate = request.ReceivedDate.Value;

        // Processing Information
        if (request.InvoiceDescription is not null)
            invoice.InvoiceDescription = request.InvoiceDescription;
        
        if (request.VoucherNumber is not null)
            invoice.VoucherNumber = request.VoucherNumber;
        
        if (request.CheckNumber is not null)
            invoice.CheckNumber = request.CheckNumber;
        
        if (request.CheckDate.HasValue)
            invoice.CheckDate = request.CheckDate.Value;

        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success(invoice);
    }
}
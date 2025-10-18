using BLH.ApproveIQ.Application.Invoices.Queries;
using BLH.ApproveIQ.Application.Invoices.Commands;
using BLH.ApproveIQ.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BLH.ApproveIQ.Presentation.Controllers;

[Route("api/[controller]")]
[Authorize]
public sealed class InvoicesController : ApiController
{
    public InvoicesController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAllInvoices(CancellationToken cancellationToken)
    {
        var query = new GetAllInvoicesQuery();
        var result = await Sender.Send(query, cancellationToken);

        return HandleResult(result);
    }

    [HttpGet("my-pending")]
    public async Task<IActionResult> GetMyPendingInvoices(CancellationToken cancellationToken)
    {
        var query = new GetMyPendingInvoicesQuery();
        var result = await Sender.Send(query, cancellationToken);

        return HandleResult(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetInvoiceById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetInvoiceByIdQuery(id);
        var result = await Sender.Send(query, cancellationToken);

        return HandleResult(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateInvoice(Guid id, [FromBody] UpdateInvoiceRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateInvoiceCommand(
            id,
            request.InvoiceNumber,
            request.ApInvoiceType,
            request.InvoiceStatus,
            request.CurrentlyAssignedToUserId,
            request.JdeOrderNumber,
            request.LegacyOrderNumber,
            request.OrderType,
            request.BuJobNumber,
            request.Category,
            request.Company,
            request.VendorId,
            request.VendorName,
            request.VendorType,
            request.VendorStreetAddress,
            request.VendorCity,
            request.VendorState,
            request.VendorZip,
            request.NetAmount,
            request.MiscAmount,
            request.FreightAmount,
            request.GrossAmount,
            request.TaxableAmount,
            request.TaxExCode,
            request.TaxArea,
            request.TaxAmount,
            request.RetainagePct,
            request.RetainageAmount,
            request.AmountToPay,
            request.Currency,
            request.PaymentTerms,
            request.PaymentHoldFlag,
            request.GlDate,
            request.InvoiceDate,
            request.ReceivedDate,
            request.InvoiceDescription,
            request.VoucherNumber,
            request.CheckNumber,
            request.CheckDate);

        var result = await Sender.Send(command, cancellationToken);

        return HandleResult(result);
    }
}

public class UpdateInvoiceRequest
{
    public string? InvoiceNumber { get; set; }
    public string? ApInvoiceType { get; set; }
    public string? InvoiceStatus { get; set; }
    public Guid? CurrentlyAssignedToUserId { get; set; }
    public string? JdeOrderNumber { get; set; }
    public string? LegacyOrderNumber { get; set; }
    public string? OrderType { get; set; }
    public string? BuJobNumber { get; set; }
    public string? Category { get; set; }
    public string? Company { get; set; }
    public string? VendorId { get; set; }
    public string? VendorName { get; set; }
    public string? VendorType { get; set; }
    public string? VendorStreetAddress { get; set; }
    public string? VendorCity { get; set; }
    public string? VendorState { get; set; }
    public string? VendorZip { get; set; }
    public decimal? NetAmount { get; set; }
    public decimal? MiscAmount { get; set; }
    public decimal? FreightAmount { get; set; }
    public decimal? GrossAmount { get; set; }
    public decimal? TaxableAmount { get; set; }
    public string? TaxExCode { get; set; }
    public string? TaxArea { get; set; }
    public decimal? TaxAmount { get; set; }
    public decimal? RetainagePct { get; set; }
    public decimal? RetainageAmount { get; set; }
    public decimal? AmountToPay { get; set; }
    public string? Currency { get; set; }
    public string? PaymentTerms { get; set; }
    public bool? PaymentHoldFlag { get; set; }
    public DateTime? GlDate { get; set; }
    public DateTime? InvoiceDate { get; set; }
    public DateTime? ReceivedDate { get; set; }
    public string? InvoiceDescription { get; set; }
    public string? VoucherNumber { get; set; }
    public string? CheckNumber { get; set; }
    public DateTime? CheckDate { get; set; }
}
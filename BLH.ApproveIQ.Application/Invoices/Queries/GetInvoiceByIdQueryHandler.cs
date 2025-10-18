using BLH.ApproveIQ.Application.Abstractions.Messaging;
using BLH.ApproveIQ.Domain.Entities;
using BLH.ApproveIQ.Domain.Repositories;
using BLH.ApproveIQ.Domain.Shared;

namespace BLH.ApproveIQ.Application.Invoices.Queries;

public sealed class GetInvoiceByIdQueryHandler : IQueryHandler<GetInvoiceByIdQuery, Invoice>
{
    private readonly IGenericRepository<Invoice> _invoiceRepository;

    public GetInvoiceByIdQueryHandler(IGenericRepository<Invoice> invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }

    public async Task<Result<Invoice>> Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
    {
        var invoice = await _invoiceRepository.GetByIdAsync(request.InvoiceId, cancellationToken);

        if (invoice is null)
        {
            return Result.Failure<Invoice>(new Error("Invoice.NotFound", $"Invoice with ID {request.InvoiceId} was not found"));
        }

        return invoice;
    }
}
using BLH.ApproveIQ.Application.Abstractions.Messaging;
using BLH.ApproveIQ.Domain.Entities;

namespace BLH.ApproveIQ.Application.Invoices.Queries;

public sealed record GetInvoiceByIdQuery(Guid InvoiceId) : IQuery<Invoice>;
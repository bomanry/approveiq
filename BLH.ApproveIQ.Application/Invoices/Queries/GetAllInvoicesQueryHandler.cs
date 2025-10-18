using BLH.ApproveIQ.Application.Abstractions.Messaging;
using BLH.ApproveIQ.Application.DTOs;
using BLH.ApproveIQ.Domain.Entities;
using BLH.ApproveIQ.Domain.Repositories;
using BLH.ApproveIQ.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace BLH.ApproveIQ.Application.Invoices.Queries;

public sealed record GetAllInvoicesQuery : IQuery<List<InvoiceListDto>>;

internal sealed class GetAllInvoicesQueryHandler(
    IGenericRepository<Invoice> invoiceRepository) 
    : IQueryHandler<GetAllInvoicesQuery, List<InvoiceListDto>>
{
    public async Task<Result<List<InvoiceListDto>>> Handle(GetAllInvoicesQuery request, CancellationToken cancellationToken)
    {
        var invoices = await invoiceRepository.GetQueryable()
            .Include(i => i.Project)
            .Include(i => i.CurrentlyAssignedToUser)
            .Select(i => new InvoiceListDto
            {
                Id = i.Id,
                InvoiceNumber = i.InvoiceNumber,
                InvoiceDate = i.InvoiceDate,
                VendorName = i.VendorName,
                VendorId = i.VendorId,
                ProjectName = i.Project != null ? i.Project.Name : null,
                BuJobNumber = i.BuJobNumber,
                GrossAmount = i.GrossAmount,
                InvoiceStatus = i.InvoiceStatus,
                AssignedUserFirstName = i.CurrentlyAssignedToUser != null ? i.CurrentlyAssignedToUser.FirstName : null,
                AssignedUserLastName = i.CurrentlyAssignedToUser != null ? i.CurrentlyAssignedToUser.LastName : null,
                AssignedUserFullName = i.CurrentlyAssignedToUser != null 
                    ? (i.CurrentlyAssignedToUser.FirstName + " " + i.CurrentlyAssignedToUser.LastName).Trim()
                    : "Unassigned"
            })
            .OrderByDescending(i => i.InvoiceDate)
            .ToListAsync(cancellationToken);

        return Result.Success(invoices);
    }
}
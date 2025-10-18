using BLH.ApproveIQ.Application.Abstractions.Messaging;
using BLH.ApproveIQ.Application.DTOs;
using BLH.ApproveIQ.Domain.Constants;
using BLH.ApproveIQ.Domain.Entities;
using BLH.ApproveIQ.Domain.Repositories;
using BLH.ApproveIQ.Domain.Services;
using BLH.ApproveIQ.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace BLH.ApproveIQ.Application.Invoices.Queries;

public sealed record GetMyPendingInvoicesQuery : IQuery<List<InvoiceListDto>>;

internal sealed class GetMyPendingInvoicesQueryHandler(
    IGenericRepository<Invoice> invoiceRepository,
    ICurrentUserService currentUserService) 
    : IQueryHandler<GetMyPendingInvoicesQuery, List<InvoiceListDto>>
{
    public async Task<Result<List<InvoiceListDto>>> Handle(GetMyPendingInvoicesQuery request, CancellationToken cancellationToken)
    {
        if (!currentUserService.UserExists || !currentUserService.UserId.HasValue)
        {
            return Result.Failure<List<InvoiceListDto>>(new Error("User.NotAuthenticated", "User is not authenticated"));
        }
        
        var invoices = await invoiceRepository.GetQueryable()
            .Where(i => i.CurrentlyAssignedToUserId == currentUserService.UserId)
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
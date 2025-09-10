using BLH.ApproveIQ.Domain.Entities;

namespace BLH.ApproveIQ.Domain.Repositories;

public interface IInvoiceItemRepository : IGenericRepository<InvoiceItem>
{
    Task<IEnumerable<InvoiceItem>> GetItemsByInvoiceIdAsync(Guid invoiceId, CancellationToken cancellationToken = default);
}
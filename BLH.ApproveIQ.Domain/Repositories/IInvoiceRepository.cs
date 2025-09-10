using BLH.ApproveIQ.Domain.Entities;

namespace BLH.ApproveIQ.Domain.Repositories;

public interface IInvoiceRepository : IGenericRepository<Invoice>
{
    Task<Invoice?> GetInvoiceWithItemsAsync(Guid invoiceId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Invoice>> GetInvoicesByStatusAsync(string status, CancellationToken cancellationToken = default);
    Task<IEnumerable<Invoice>> GetInvoicesByVendorAsync(string vendorId, CancellationToken cancellationToken = default);
}
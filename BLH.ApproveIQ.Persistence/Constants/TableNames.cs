using BLH.ApproveIQ.Domain.Entities;

namespace BLH.ApproveIQ.Persistence.Constants;

internal static class TableNames
{
    internal const string AuditLogs = nameof(AuditLogs);
    internal const string B2CUsers = nameof(B2CUsers);
    internal const string ExceptionLogs = nameof(ExceptionLogs);
    internal const string InvoiceItems = nameof(InvoiceItems);
    internal const string Invoices = nameof(Invoices);
    internal const string OutboxMessageConsumers = nameof(OutboxMessageConsumers);
    internal const string OutboxMessages = nameof(OutboxMessages);
    internal const string Users = nameof(Users);
}

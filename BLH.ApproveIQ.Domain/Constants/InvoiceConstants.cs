namespace BLH.ApproveIQ.Domain.Constants;

public static class InvoiceStatuses
{
    public const string New = "New";
    public const string PendingApproval = "PendingApproval";
    public const string Approved = "Approved";
    public const string Rejected = "Rejected";
    public const string Paid = "Paid";
    public const string OnHold = "On Hold";
}

public static class ApprovalActions
{
    public const string Assigned = "Assigned";
    public const string Approved = "Approved";
    public const string Rejected = "Rejected";
    public const string Reassigned = "Reassigned";
    public const string PutOnHold = "Put On Hold";
    public const string Released = "Released";
}

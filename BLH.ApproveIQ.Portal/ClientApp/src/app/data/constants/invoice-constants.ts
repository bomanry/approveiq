export class InvoiceStatuses {
  static readonly New = 'New';
  static readonly PendingApproval = 'Pending Approval';
  static readonly Approved = 'Approved';
  static readonly Rejected = 'Rejected';
  static readonly Paid = 'Paid';
  static readonly OnHold = 'On Hold';
}

export class ApprovalActions {
  static readonly Assigned = 'Assigned';
  static readonly Approved = 'Approved';
  static readonly Rejected = 'Rejected';
  static readonly Reassigned = 'Reassigned';
  static readonly PutOnHold = 'Put On Hold';
  static readonly Released = 'Released';
}

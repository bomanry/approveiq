import { Component, OnInit } from '@angular/core';
import { Invoice } from '../../../data/data/b-l-h/entities/invoice';
import { InvoiceService } from '../../../services/invoice.service';
import { InvoiceStatuses, ApprovalActions } from '../../../data/constants/invoice-constants';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  pendingInvoices: Invoice[] = [];
  invoiceStatuses = InvoiceStatuses;
  approvalActions = ApprovalActions;
  loading = false;

  // Computed properties for the summary cards
  pendingInvoicesCount = 0;
  newInvoicesCount = 0;
  pendingApprovalCount = 0;
  totalValue = 0;

  constructor(private invoiceService: InvoiceService) { }

  ngOnInit(): void {
    this.loadPendingInvoices();
  }

  loadPendingInvoices(): void {
    this.loading = true;
    // For now, using mock data until the service is properly connected
    // this.invoiceService.getMyPendingInvoices().subscribe({
    //   next: (invoices) => {
    //     this.pendingInvoices = invoices;
    //     this.updateCounts();
    //     this.loading = false;
    //   },
    //   error: (error) => {
    //     console.error('Error loading invoices:', error);
    //     this.loading = false;
    //   }
    // });

    // Mock data for testing
    setTimeout(() => {
      this.pendingInvoices = this.getMockInvoices();
      this.updateCounts();
      this.loading = false;
    }, 1000);
  }

  private updateCounts(): void {
    this.pendingInvoicesCount = this.pendingInvoices.length;
    this.newInvoicesCount = this.pendingInvoices.filter(i => i.invoiceStatus === InvoiceStatuses.New).length;
    this.pendingApprovalCount = this.pendingInvoices.filter(i => i.invoiceStatus === InvoiceStatuses.PendingApproval).length;
    this.totalValue = this.pendingInvoices.reduce((sum, i) => sum + (i.grossAmount || 0), 0);
  }

  approveInvoice(invoice: Invoice): void {
    if (confirm(`Are you sure you want to approve invoice ${invoice.vendorName} for $${invoice.grossAmount}?`)) {
      // For now, just update locally until service is connected
      invoice.invoiceStatus = InvoiceStatuses.Approved;
      this.pendingInvoices = this.pendingInvoices.filter(i => i.id !== invoice.id);
      this.updateCounts();

      // Uncomment when service is ready:
      // this.invoiceService.updateInvoiceStatus(invoice.id, InvoiceStatuses.Approved)
      //   .subscribe(() => {
      //     this.loadPendingInvoices();
      //   });
    }
  }

  rejectInvoice(invoice: Invoice): void {
    if (confirm(`Are you sure you want to reject invoice ${invoice.vendorName} for $${invoice.grossAmount}?`)) {
      // For now, just update locally until service is connected
      invoice.invoiceStatus = InvoiceStatuses.Rejected;
      this.pendingInvoices = this.pendingInvoices.filter(i => i.id !== invoice.id);
      this.updateCounts();

      // Uncomment when service is ready:
      // this.invoiceService.updateInvoiceStatus(invoice.id, InvoiceStatuses.Rejected)
      //   .subscribe(() => {
      //     this.loadPendingInvoices();
      //   });
    }
  }

  reassignInvoice(invoice: Invoice): void {
    // TODO: Implement reassignment dialog
    console.log('Reassign invoice:', invoice);
  }

  getStatusBadgeClass(status: string): string {
    switch (status) {
      case InvoiceStatuses.New:
        return 'badge badge-primary';
      case InvoiceStatuses.PendingApproval:
        return 'badge badge-warning';
      case InvoiceStatuses.Approved:
        return 'badge badge-success';
      case InvoiceStatuses.Rejected:
        return 'badge badge-danger';
      case InvoiceStatuses.OnHold:
        return 'badge badge-secondary';
      default:
        return 'badge badge-light';
    }
  }

  private getMockInvoices(): Invoice[] {
    // Mock data that simulates your test invoices
    return [
      {
        id: '1',
        vendorName: 'ABC Construction',
        vendorId: 'ABC001',
        invoiceDate: new Date('2025-01-05'),
        grossAmount: 15000.00,
        invoiceStatus: InvoiceStatuses.New,
        buJobNumber: 'PROJ-2025-001',
        project: { name: 'Office Building Renovation', buJobNumber: 'PROJ-2025-001' } as any
      } as Invoice,
      {
        id: '2',
        vendorName: 'XYZ Materials',
        vendorId: 'XYZ002',
        invoiceDate: new Date('2025-01-03'),
        grossAmount: 8500.00,
        invoiceStatus: InvoiceStatuses.PendingApproval,
        buJobNumber: 'PROJ-2025-002',
        project: { name: 'Warehouse Expansion', buJobNumber: 'PROJ-2025-002' } as any
      } as Invoice
    ];
  }
}

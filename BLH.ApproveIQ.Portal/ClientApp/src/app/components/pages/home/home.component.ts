import { Component, OnInit } from '@angular/core';
import { Invoice } from '../../../data/data/b-l-h/entities/invoice';
import { InvoiceListDto } from '../../../data/data/b-l-h/dtos/invoice-list-dto';
import { InvoiceService } from '../../../services/invoice.service';
import { InvoiceStatuses, ApprovalActions } from '../../../data/constants/invoice-constants';
import PrimeNgTableColumn from 'src/app/framework/models/primeNgTableColumn';
import { ComponentBase } from 'src/app/framework/models/componentBase';
import { LazyLoadEvent } from 'primeng/api';

@Component({

  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent extends ComponentBase implements OnInit {
  pendingInvoices: InvoiceListDto[] = [];
  invoiceStatuses = InvoiceStatuses;
  approvalActions = ApprovalActions;
  loading = false;
  totalRecords = 0;
  cols: PrimeNgTableColumn[] = [];
  globalFilterFields: string[] = [];
  actionLinks: any[] = [];

  // Computed properties for the summary cards
  pendingInvoicesCount = 0;
  newInvoicesCount = 0;
  pendingApprovalCount = 0;
  totalValue = 0;

  constructor(private invoiceService: InvoiceService) {
    super();
    this.setupTableColumns();
    this.setupActionLinks();
  }

  ngOnInit(): void {
    this.loadPendingInvoices();
  }

  setupTableColumns(): void {
    this.cols = [
      {
        field: 'invoiceDate',
        header: 'Invoice Date',
        type: 'Date',
        dateFormat: 'shortDate',
        filterType: 'date',
        style: 'min-width: 120px;'
      },
      {
        field: 'vendorName',
        header: 'Vendor',
        filterType: 'text',
        style: 'min-width: 200px;'
      },
      {
        field: 'projectName',
        header: 'Project',
        filterType: 'text',
        style: 'min-width: 150px;'
      },
      {
        field: 'grossAmount',
        header: 'Amount',
        type: 'Currency',
        currencyFormat: 'USD',
        filterType: 'numeric',
        style: 'min-width: 120px; text-align: right;'
      },
      {
        field: 'invoiceStatus',
        header: 'Status',
        filterType: 'text',
        style: 'min-width: 120px;'
      },
      {
        field: 'assignedUserFullName',
        header: 'Assigned To',
        filterType: 'text',
        style: 'min-width: 150px;'
      }
    ];

    // Setup global filter fields for search
    this.globalFilterFields = [
      'vendorName',
      'vendorId',
      'projectName',
      'buJobNumber',
      'invoiceStatus',
      'assignedUserFirstName',
      'assignedUserLastName',
      'assignedUserFullName'
    ];
  }

  setupActionLinks(): void {
    this.actionLinks = [
      {
        label: 'Refresh',
        tooltip: 'Refresh invoice list',
        icon: 'pi-refresh',
        action: () => { this.loadPendingInvoices(); }
      }
    ];
  }

  loadPendingInvoices(): void {
    this.loading = true;
    this.invoiceService.getMyPendingInvoices()
      .pipe(this.takeUntilDestroy())
      .subscribe({
        next: (invoices) => {
          this.pendingInvoices = invoices;
          this.totalRecords = this.pendingInvoices.length;
          this.updateCounts();
          this.loading = false;
        },
        error: (error) => {
          console.error('Error loading invoices:', error);
          // Fallback to mock data if API fails
          this.pendingInvoices = this.getMockInvoices();
          this.totalRecords = this.pendingInvoices.length;
          this.updateCounts();
          this.loading = false;
        }
      });
  }


  private updateCounts(): void {
    this.pendingInvoicesCount = this.pendingInvoices.length;
    this.newInvoicesCount = this.pendingInvoices.filter(i => i.invoiceStatus === InvoiceStatuses.New).length;
    this.pendingApprovalCount = this.pendingInvoices.filter(i => i.invoiceStatus === InvoiceStatuses.PendingApproval).length;
    this.totalValue = this.pendingInvoices.reduce((sum, i) => sum + (i.grossAmount || 0), 0);
  }

  approveInvoice(invoice: InvoiceListDto): void {
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

  rejectInvoice(invoice: InvoiceListDto): void {
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

  reassignInvoice(invoice: InvoiceListDto): void {
    // TODO: Implement reassignment dialog
    console.log('Reassign invoice:', invoice);
  }

  reviewInvoice(invoice: InvoiceListDto): void {
    // Navigate to invoice detail page
    this.router.navigate(['/invoice', invoice.id]);
  }

  onLazyLoad(event: LazyLoadEvent): void {
    // For now, we're loading all data client-side
    console.log('Lazy load event:', event);
  }

  onEditClicked(invoice: InvoiceListDto): void {
    this.reviewInvoice(invoice);
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

  private getMockInvoices(): InvoiceListDto[] {
    // Mock data that simulates your test invoices
    return [
      {
        id: '1',
        invoiceNumber: 'INV-001',
        vendorName: 'ABC Construction',
        vendorId: 'ABC001',
        invoiceDate: new Date('2025-01-05'),
        grossAmount: 15000.00,
        invoiceStatus: InvoiceStatuses.PendingApproval,
        buJobNumber: 'PROJ-2025-001',
        projectName: 'Office Building Renovation',
        assignedUserFirstName: 'John',
        assignedUserLastName: 'Smith',
        assignedUserFullName: 'John Smith'
      } as InvoiceListDto,
      {
        id: '2',
        invoiceNumber: 'INV-002',
        vendorName: 'XYZ Materials',
        vendorId: 'XYZ002',
        invoiceDate: new Date('2025-01-03'),
        grossAmount: 8500.00,
        invoiceStatus: InvoiceStatuses.New,
        buJobNumber: 'PROJ-2025-002',
        projectName: 'Warehouse Expansion',
        assignedUserFirstName: 'Jane',
        assignedUserLastName: 'Doe',
        assignedUserFullName: 'Jane Doe'
      } as InvoiceListDto
    ];
  }
}

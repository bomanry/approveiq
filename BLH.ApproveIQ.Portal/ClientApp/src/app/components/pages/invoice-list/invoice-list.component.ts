import { Component, OnInit } from '@angular/core';
import { Invoice } from '../../../data/data/b-l-h/entities/invoice';
import { InvoiceListDto } from '../../../data/data/b-l-h/dtos/invoice-list-dto';
import { InvoiceService } from '../../../services/invoice.service';
import { MessageService, LazyLoadEvent } from 'primeng/api';
import PrimeNgTableColumn from 'src/app/framework/models/primeNgTableColumn';
import { ComponentBase } from 'src/app/framework/models/componentBase';
import { InvoiceStatuses } from '../../../data/constants/invoice-constants';

@Component({
  selector: 'app-invoice-list',
  templateUrl: './invoice-list.component.html',
  styleUrls: ['./invoice-list.component.scss']
})
export class InvoiceListComponent extends ComponentBase implements OnInit {
  invoices: InvoiceListDto[] = [];
  loading = false;
  totalRecords = 0;
  cols: PrimeNgTableColumn[] = [];
  globalFilterFields: string[] = [];

  // Download dialog properties
  displayDownloadDialog = false;
  downloadFormat: 'csv' | 'excel' = 'csv';
  includePdf = false;

  // Action links for custom buttons
  actionLinks: any[] = [];

  constructor(
    private invoiceService: InvoiceService,
    private messageService: MessageService
  ) {
    super();
    this.setupTableColumns();
    this.setupActionLinks();
  }

  ngOnInit(): void {
    this.loadInvoices();
  }

  setupTableColumns(): void {
    this.cols = [
      {
        field: 'invoiceNumber',
        header: 'Invoice Number',
        filterType: 'text',
        style: 'min-width: 150px;'
      },
      {
        field: 'invoiceDate',
        header: 'Date',
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
      'invoiceNumber',
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
        label: 'Download',
        tooltip: 'Download invoice data',
        icon: 'pi-download',
        action: () => this.showDownloadDialog()
      }
    ];
  }

  loadInvoices(): void {
    this.loading = true;
    // Get ALL invoices (not just mine) for the invoice list page
    this.invoiceService.getInvoices()
      .pipe(this.takeUntilDestroy())
      .subscribe({
        next: (invoices) => {
          this.invoices = invoices;
          this.totalRecords = this.invoices.length;
          this.loading = false;
        },
        error: (error) => {
          console.error('Error loading invoices:', error);
          this.messageService.add({
            severity: 'error',
            summary: 'Error',
            detail: 'Failed to load invoices from database'
          });
          this.loading = false;
        }
      });
  }


  onLazyLoad(event: LazyLoadEvent): void {
    // For now, we're loading all data client-side
    // This could be enhanced to support server-side paging/filtering
    console.log('Lazy load event:', event);
  }

  viewInvoice(invoice: InvoiceListDto): void {
    this.router.navigate(['/invoice', invoice.id]);
  }

  onEditClicked(invoice: InvoiceListDto): void {
    this.viewInvoice(invoice);
  }

  showDownloadDialog(): void {
    this.displayDownloadDialog = true;
  }

  performDownload(): void {
    // Close the dialog
    this.displayDownloadDialog = false;

    // Show a message indicating download is starting
    this.messageService.add({
      severity: 'info',
      summary: 'Download Started',
      detail: `Preparing ${this.downloadFormat.toUpperCase()} download${this.includePdf ? ' with PDFs' : ''}...`
    });

    // TODO: Implement actual download logic based on format and includePdf option
    // For now, we'll just log the selections
    console.log('Download format:', this.downloadFormat);
    console.log('Include PDF:', this.includePdf);
    console.log('Invoice count:', this.invoices.length);

    // Simulate download completion
    setTimeout(() => {
      this.messageService.add({
        severity: 'success',
        summary: 'Download Complete',
        detail: 'Your invoice data has been downloaded successfully'
      });
    }, 1500);
  }
}
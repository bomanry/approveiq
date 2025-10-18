import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { Invoice } from '../data/data/b-l-h/entities/invoice';
import { InvoiceListDto } from '../data/data/b-l-h/dtos/invoice-list-dto';
import { InvoiceStatuses } from '../data/constants/invoice-constants';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class InvoiceService {
  private readonly baseUrl = environment.baseUrl;

  constructor(private http: HttpClient) {
  }

  /**
   * Get invoices assigned to the current user that need review
   */
  getMyPendingInvoices(): Observable<InvoiceListDto[]> {
    return this.http.get<InvoiceListDto[]>(`${this.baseUrl}/invoices/my-pending`);
  }

  /**
   * Get a specific invoice by ID
   */
  getInvoiceById(invoiceId: string): Observable<Invoice> {
    return this.http.get<Invoice>(`${this.baseUrl}/invoices/${invoiceId}`);
  }

  /**
   * Update an invoice
   */
  updateInvoice(invoice: Invoice): Observable<Invoice> {
    return this.http.put<Invoice>(`${this.baseUrl}/invoices/${invoice.id}`, invoice);
  }

  /**
   * Get all invoices assigned to the current user
   */
  getMyInvoices(): Observable<Invoice[]> {
    // TODO: Replace with actual API call
    // return this.http.get<Invoice[]>(`/api/invoices/my-invoices?userId=${this.getCurrentUserId()}`);

    // For now, return mock data
    return of([]);
  }

  /**
   * Get all invoices for the invoice list page (not just mine)
   */
  getInvoices(): Observable<InvoiceListDto[]> {
    return this.http.get<InvoiceListDto[]>(`${this.baseUrl}/invoices`);
  }

  /**
   * Update invoice status and assignment
   */
  updateInvoiceStatus(invoiceId: string, status: string, assignToUserId?: string): Observable<Invoice> {
    // TODO: Replace with actual API call
    // const updateData: any = { invoiceStatus: status };
    // if (assignToUserId) {
    //   updateData.currentlyAssignedToUserId = assignToUserId;
    // }
    // return this.http.put<Invoice>(`/api/invoices/${invoiceId}`, updateData);

    // For now, return mock response
    return of({} as Invoice);
  }

  private getMockInvoiceById(invoiceId: string): Invoice {
    // Mock data that matches the structure from your image
    const mockInvoice = new Invoice();
    mockInvoice.id = invoiceId;

    // Invoice Info section
    mockInvoice.apInvoiceType = 'PO';
    mockInvoice.invoiceStatus = 'Pending Approval';
    mockInvoice.jdeOrderNumber = '';
    mockInvoice.legacyOrderNumber = '';
    mockInvoice.orderType = '';
    mockInvoice.buJobNumber = '10335';
    mockInvoice.category = '';
    mockInvoice.company = '10300';

    // Vendor Info section
    mockInvoice.vendorId = '66173';
    mockInvoice.vendorName = 'KELVIN REDD, LLC';
    mockInvoice.vendorType = 'V';
    mockInvoice.vendorStreetAddress = '2408 SUMMERVILLE ROAD STE155 #473';
    mockInvoice.vendorCity = 'PHENIX CITY';
    mockInvoice.vendorState = 'AL';
    mockInvoice.vendorZip = '36867';

    // Date Info section
    mockInvoice.glDate = new Date('2023-07-02');
    mockInvoice.invoiceDate = new Date('2023-07-02');
    mockInvoice.receivedDate = new Date();

    // Amount Info section
    mockInvoice.netAmount = 199.00;
    mockInvoice.miscAmount = 0.00;
    mockInvoice.freightAmount = 0.00;
    mockInvoice.grossAmount = 199.00;
    mockInvoice.taxableAmount = 0.00;
    mockInvoice.taxExCode = '';
    mockInvoice.taxArea = '';
    mockInvoice.taxAmount = 0.00;
    mockInvoice.retainagePct = 0.00000;
    mockInvoice.retainageAmount = 0.00000;
    mockInvoice.amountToPay = 199.00;
    mockInvoice.currency = 'USD';
    mockInvoice.paymentTerms = 'Net 15 Days';
    mockInvoice.paymentHoldFlag = false;

    // Processing Info section
    mockInvoice.invoiceDescription = 'SUPPLIES';
    mockInvoice.voucherNumber = '';
    mockInvoice.checkNumber = '';
    mockInvoice.checkDate = new Date();

    // Initialize empty lines array
    mockInvoice.lines = [];

    return mockInvoice;
  }

  private getCurrentUserId(): string {
    // TODO: Get the current user ID from auth service or user context
    return 'current-user-id';
  }
}

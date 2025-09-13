import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Invoice } from '../data/data/b-l-h/entities/invoice';
import { InvoiceStatuses } from '../data/constants/invoice-constants';

@Injectable({
  providedIn: 'root'
})
export class InvoiceService {

  constructor() {
    // TODO: Inject HttpClient when ready to connect to API
    // constructor(private http: HttpClient) {}
  }

  /**
   * Get invoices assigned to the current user that need review
   */
  getMyPendingInvoices(): Observable<Invoice[]> {
    // TODO: Replace with actual API call
    // return this.http.get<Invoice[]>(`/api/invoices/my-pending?userId=${this.getCurrentUserId()}`);

    // For now, return mock data
    return of([]);
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

  private getCurrentUserId(): string {
    // TODO: Get the current user ID from auth service or user context
    // For now, returning a placeholder - you'll need to implement this based on your auth setup
    return 'current-user-id';
  }
}

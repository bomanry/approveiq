import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

export interface PdfData {
  pdfData: string;
  fileName: string;
}

@Injectable({
  providedIn: 'root'
})
export class DocumentService {
  private readonly apiUrl = environment.baseUrl;

  constructor(private http: HttpClient) {}

  getInvoicePdf(fileName: string): Observable<Blob> {
    return this.http.get(`${this.apiUrl}/documents/invoice-pdf/${fileName}`, {
      responseType: 'blob'
    });
  }

  getInvoicePdfAsBase64(fileName: string): Observable<PdfData> {
    return this.http.get<PdfData>(`${this.apiUrl}/documents/invoice-pdf-base64/${fileName}`);
  }

  getInvoicePdfUrl(fileName: string): string {
    return `${this.apiUrl}/documents/invoice-pdf/${fileName}`;
  }

  getAvailableInvoicePdfs(): Observable<string[]> {
    return this.http.get<string[]>(`${this.apiUrl}/documents/invoice-pdfs`);
  }
}
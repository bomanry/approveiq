import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Invoice } from '../../../data/data/b-l-h/entities/invoice';
import { InvoiceItem } from '../../../data/data/b-l-h/entities/invoice-item';
import { User } from '../../../data/data/b-l-h/entities/user';
import { InvoiceService } from '../../../services/invoice.service';
import { DocumentService } from '../../../services/document.service';
import { UserService } from '../../../services/user.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ConfirmationService, MessageService } from 'primeng/api';

// PDF.js imports
import * as pdfjsLib from 'pdfjs-dist';

// Configure PDF.js worker
(pdfjsLib as any).GlobalWorkerOptions.workerSrc = 'https://cdnjs.cloudflare.com/ajax/libs/pdf.js/3.4.120/pdf.worker.min.js';

@Component({
  selector: 'app-invoice-detail',
  templateUrl: './invoice-detail.component.html',
  styleUrls: ['./invoice-detail.component.scss']
})
export class InvoiceDetailComponent implements OnInit {
  invoice: Invoice = new Invoice();
  invoiceForm: FormGroup;
  loading = false;
  saving = false;
  activeTab = 0;

  // Form groups for different tabs
  invoiceInfoForm: FormGroup;
  vendorInfoForm: FormGroup;
  amountInfoForm: FormGroup;
  dateInfoForm: FormGroup;
  processingInfoForm: FormGroup;

  // Invoice line items
  displayAddItemDialog = false;
  newItemForm: FormGroup;
  editingItemIndex: number | null = null;

  // Users for assignment
  users: User[] = [];
  userOptions: { label: string; value: string }[] = [];

  // PDF.js functionality
  @ViewChild('pdfCanvas', { static: false }) pdfCanvas!: ElementRef<HTMLCanvasElement>;
  pdfDocument: any | null = null;
  currentPage = 1;
  totalPages = 0;
  scale = 1.5;
  pdfUrl: string | null = null;
  pdfLoadError = false;
  pdfLoading = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private invoiceService: InvoiceService,
    private documentService: DocumentService,
    private userService: UserService,
    private fb: FormBuilder,
    private confirmationService: ConfirmationService,
    private messageService: MessageService
  ) {
    this.initializeForms();
  }

  ngOnInit(): void {
    const invoiceId = this.route.snapshot.paramMap.get('id');
    if (invoiceId) {
      this.loadInvoice(invoiceId);
    }
    this.loadUsers();
  }

  private initializeForms(): void {
    this.invoiceInfoForm = this.fb.group({
      invoiceNumber: ['', Validators.required],
      apInvoiceType: [''],
      invoiceStatus: [''],
      currentlyAssignedToUserId: [''],
      jdeOrderNumber: [''],
      legacyOrderNumber: [''],
      orderType: [''],
      buJobNumber: [''],
      category: [''],
      company: ['']
    });

    this.vendorInfoForm = this.fb.group({
      vendorId: ['', Validators.required],
      vendorName: ['', Validators.required],
      vendorType: [''],
      vendorStreetAddress: [''],
      vendorCity: [''],
      vendorState: [''],
      vendorZip: ['']
    });

    this.amountInfoForm = this.fb.group({
      netAmount: [0],
      miscAmount: [0],
      freightAmount: [0],
      grossAmount: [0],
      taxableAmount: [0],
      taxExCode: [''],
      taxArea: [''],
      taxAmount: [0],
      retainagePct: [0],
      retainageAmount: [0],
      amountToPay: [0],
      currency: ['USD'],
      paymentTerms: [''],
      paymentHoldFlag: [false]
    });

    this.dateInfoForm = this.fb.group({
      glDate: [new Date()],
      invoiceDate: [new Date()],
      receivedDate: [new Date()]
    });

    this.processingInfoForm = this.fb.group({
      invoiceDescription: [''],
      voucherNumber: [''],
      checkNumber: [''],
      checkDate: [new Date()]
    });

    this.newItemForm = this.fb.group({
      businessUnit: ['', Validators.required],
      buDescription: [''],
      costCode: ['', Validators.required],
      costCodeDesc: [''],
      costType: [''],
      costTypeDesc: [''],
      orderNumber: [''],
      line: [''],
      orderSuffix: [''],
      glLineType: [''],
      qty: [1, [Validators.required, Validators.min(0)]],
      uom: [''],
      unitPrice: [0, [Validators.required, Validators.min(0)]],
      amount: [0]
    });

    // Calculate amount when qty or unitPrice changes
    this.newItemForm.get('qty')?.valueChanges.subscribe(() => this.calculateItemAmount());
    this.newItemForm.get('unitPrice')?.valueChanges.subscribe(() => this.calculateItemAmount());
  }

  private loadInvoice(invoiceId: string): void {
    this.loading = true;
    this.invoiceService.getInvoiceById(invoiceId).subscribe({
      next: (invoice: Invoice) => {
        this.invoice = invoice;
        this.populateForms();
        this.loading = false;
      },
      error: (error: any) => {
        console.error('Error loading invoice:', error);
        this.messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: 'Failed to load invoice details'
        });
        this.loading = false;
      }
    });
  }

  private populateForms(): void {
    this.invoiceInfoForm.patchValue({
      invoiceNumber: this.invoice.invoiceNumber,
      apInvoiceType: this.invoice.apInvoiceType,
      invoiceStatus: this.invoice.invoiceStatus,
      currentlyAssignedToUserId: this.invoice.currentlyAssignedToUserId,
      jdeOrderNumber: this.invoice.jdeOrderNumber,
      legacyOrderNumber: this.invoice.legacyOrderNumber,
      orderType: this.invoice.orderType,
      buJobNumber: this.invoice.buJobNumber,
      category: this.invoice.category,
      company: this.invoice.company
    });

    this.vendorInfoForm.patchValue({
      vendorId: this.invoice.vendorId,
      vendorName: this.invoice.vendorName,
      vendorType: this.invoice.vendorType,
      vendorStreetAddress: this.invoice.vendorStreetAddress,
      vendorCity: this.invoice.vendorCity,
      vendorState: this.invoice.vendorState,
      vendorZip: this.invoice.vendorZip
    });

    this.amountInfoForm.patchValue({
      netAmount: this.invoice.netAmount,
      miscAmount: this.invoice.miscAmount,
      freightAmount: this.invoice.freightAmount,
      grossAmount: this.invoice.grossAmount,
      taxableAmount: this.invoice.taxableAmount,
      taxExCode: this.invoice.taxExCode,
      taxArea: this.invoice.taxArea,
      taxAmount: this.invoice.taxAmount,
      retainagePct: this.invoice.retainagePct,
      retainageAmount: this.invoice.retainageAmount,
      amountToPay: this.invoice.amountToPay,
      currency: this.invoice.currency,
      paymentTerms: this.invoice.paymentTerms,
      paymentHoldFlag: this.invoice.paymentHoldFlag
    });

    this.dateInfoForm.patchValue({
      glDate: this.invoice.glDate,
      invoiceDate: this.invoice.invoiceDate,
      receivedDate: this.invoice.receivedDate
    });

    this.processingInfoForm.patchValue({
      invoiceDescription: this.invoice.invoiceDescription,
      voucherNumber: this.invoice.voucherNumber,
      checkNumber: this.invoice.checkNumber,
      checkDate: this.invoice.checkDate
    });
  }

  showAddItemDialog(): void {
    this.editingItemIndex = null;
    this.newItemForm.reset();
    this.newItemForm.patchValue({
      qty: 1,
      unitPrice: 0,
      amount: 0
    });
    this.displayAddItemDialog = true;
  }

  editInvoiceItem(index: number): void {
    this.editingItemIndex = index;
    const item = this.invoice.lines[index];
    this.newItemForm.patchValue({
      businessUnit: item.businessUnit,
      buDescription: item.buDescription,
      costCode: item.costCode,
      costCodeDesc: item.costCodeDesc,
      costType: item.costType,
      costTypeDesc: item.costTypeDesc,
      orderNumber: item.orderNumber,
      line: item.line,
      orderSuffix: item.orderSuffix,
      glLineType: item.glLineType,
      qty: item.qty,
      uom: item.uom,
      unitPrice: item.unitPrice,
      amount: item.amount
    });
    this.displayAddItemDialog = true;
  }

  addInvoiceItem(): void {
    if (this.newItemForm.valid) {
      if (this.editingItemIndex !== null) {
        // Update existing item
        Object.assign(this.invoice.lines[this.editingItemIndex], this.newItemForm.value);
        this.messageService.add({
          severity: 'success',
          summary: 'Success',
          detail: 'Invoice item updated successfully'
        });
        this.editingItemIndex = null;
      } else {
        // Add new item
        const newItem = new InvoiceItem();
        Object.assign(newItem, this.newItemForm.value);
        newItem.invoiceId = this.invoice.id;
        this.invoice.lines.push(newItem);
        this.messageService.add({
          severity: 'success',
          summary: 'Success',
          detail: 'Invoice item added successfully'
        });
      }
      this.displayAddItemDialog = false;
    }
  }

  removeInvoiceItem(index: number): void {
    this.confirmationService.confirm({
      message: 'Are you sure you want to remove this item?',
      accept: () => {
        this.invoice.lines.splice(index, 1);
        this.messageService.add({
          severity: 'success',
          summary: 'Success',
          detail: 'Invoice item removed successfully'
        });
      }
    });
  }

  private calculateItemAmount(): void {
    const qty = this.newItemForm.get('qty')?.value || 0;
    const unitPrice = this.newItemForm.get('unitPrice')?.value || 0;
    const amount = qty * unitPrice;
    this.newItemForm.get('amount')?.setValue(amount, { emitEvent: false });
  }

  saveInvoice(): void {
    console.log('Save button clicked');
    console.log('Form validity:', {
      invoiceInfo: this.invoiceInfoForm.valid,
      vendorInfo: this.vendorInfoForm.valid,
      amountInfo: this.amountInfoForm.valid,
      dateInfo: this.dateInfoForm.valid,
      processingInfo: this.processingInfoForm.valid
    });
    
    if (this.isFormValid()) {
      console.log('All forms valid, proceeding with save');
      this.saving = true;

      // Update invoice object with form values
      this.updateInvoiceFromForms();

      console.log('Calling updateInvoice API with:', this.invoice);
      this.invoiceService.updateInvoice(this.invoice).subscribe({
        next: () => {
          console.log('Save successful');
          this.messageService.add({
            severity: 'success',
            summary: 'Success',
            detail: 'Invoice saved successfully'
          });
          this.saving = false;
        },
        error: (error) => {
          console.error('Error saving invoice:', error);
          this.messageService.add({
            severity: 'error',
            summary: 'Error',
            detail: 'Failed to save invoice'
          });
          this.saving = false;
        }
      });
    } else {
      console.log('Form validation failed, not saving');
      this.messageService.add({
        severity: 'warn',
        summary: 'Validation Error',
        detail: 'Please fix form errors before saving'
      });
    }
  }

  private isFormValid(): boolean {
    return this.invoiceInfoForm.valid &&
           this.vendorInfoForm.valid &&
           this.amountInfoForm.valid &&
           this.dateInfoForm.valid &&
           this.processingInfoForm.valid;
  }

  private updateInvoiceFromForms(): void {
    Object.assign(this.invoice, this.invoiceInfoForm.value);
    Object.assign(this.invoice, this.vendorInfoForm.value);
    Object.assign(this.invoice, this.amountInfoForm.value);
    Object.assign(this.invoice, this.dateInfoForm.value);
    Object.assign(this.invoice, this.processingInfoForm.value);
  }

  private loadUsers(): void {
    this.userService.getAllUsers().subscribe({
      next: (users: User[]) => {
        this.users = users;
        this.userOptions = users.map(user => ({
          label: `${user.firstName} ${user.lastName}`,
          value: user.id
        }));
      },
      error: (error: any) => {
        console.error('Error loading users:', error);
        this.messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: 'Failed to load users for assignment'
        });
      }
    });
  }


  async showPdf(): Promise<void> {
    if (this.invoice.pdfFileName) {
      this.pdfUrl = this.documentService.getInvoicePdfUrl(this.invoice.pdfFileName);
      this.pdfLoadError = false;
      this.pdfLoading = true;
      
      try {
        // Load the PDF document
        const loadingTask = pdfjsLib.getDocument(this.pdfUrl);
        this.pdfDocument = await loadingTask.promise;
        this.totalPages = this.pdfDocument.numPages;
        this.currentPage = 1;
        
        // Render the first page
        setTimeout(() => this.renderPage(this.currentPage), 100);
      } catch (error) {
        console.error('Error loading PDF:', error);
        this.pdfLoadError = true;
      } finally {
        this.pdfLoading = false;
      }
    }
  }

  hidePdf(): void {
    this.pdfUrl = null;
    this.pdfLoadError = false;
    this.pdfDocument = null;
    this.currentPage = 1;
    this.totalPages = 0;
  }

  async renderPage(pageNumber: number): Promise<void> {
    if (!this.pdfDocument || !this.pdfCanvas) {
      console.log('Missing pdfDocument or pdfCanvas');
      return;
    }

    try {
      const page: any = await this.pdfDocument.getPage(pageNumber);
      const canvas = this.pdfCanvas.nativeElement;
      const context = canvas.getContext('2d')!;
      
      // Clear the canvas first
      context.clearRect(0, 0, canvas.width, canvas.height);
      
      const viewport = page.getViewport({ scale: this.scale });
      canvas.height = viewport.height;
      canvas.width = viewport.width;
      
      console.log(`Rendering page ${pageNumber} at scale ${this.scale}, canvas size: ${canvas.width}x${canvas.height}`);
      
      const renderContext = {
        canvasContext: context,
        viewport: viewport
      };
      
      await page.render(renderContext).promise;
    } catch (error) {
      console.error('Error rendering page:', error);
      this.pdfLoadError = true;
    }
  }

  previousPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.renderPage(this.currentPage);
    }
  }

  nextPage(): void {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
      this.renderPage(this.currentPage);
    }
  }

  zoomIn(): void {
    this.scale += 0.25;
    console.log(`Zoom In - New scale: ${this.scale}`);
    this.renderPage(this.currentPage);
  }

  zoomOut(): void {
    if (this.scale > 0.5) {
      this.scale -= 0.25;
      console.log(`Zoom Out - New scale: ${this.scale}`);
      this.renderPage(this.currentPage);
    } else {
      console.log('Cannot zoom out further - minimum scale reached');
    }
  }

  fitToWidth(): void {
    // Calculate scale to fit width of container
    const container = this.pdfCanvas?.nativeElement.parentElement;
    if (container && this.pdfDocument) {
      this.pdfDocument.getPage(this.currentPage).then((page: any) => {
        const viewport = page.getViewport({ scale: 1 });
        const containerWidth = container.clientWidth - 20; // Account for padding
        this.scale = containerWidth / viewport.width;
        this.renderPage(this.currentPage);
      });
    }
  }

  downloadPdf(): void {
    if (this.invoice.pdfFileName) {
      this.documentService.getInvoicePdf(this.invoice.pdfFileName).subscribe({
        next: (blob: Blob) => {
          const url = window.URL.createObjectURL(blob);
          const link = document.createElement('a');
          link.href = url;
          link.download = this.invoice.pdfFileName!;
          link.click();
          window.URL.revokeObjectURL(url);
        },
        error: (error: any) => {
          console.error('Error downloading PDF:', error);
          this.messageService.add({
            severity: 'error',
            summary: 'Error',
            detail: 'Failed to download PDF document'
          });
        }
      });
    }
  }

  openPdfInNewTab(): void {
    if (this.invoice.pdfFileName) {
      const pdfUrl = this.documentService.getInvoicePdfUrl(this.invoice.pdfFileName);
      window.open(pdfUrl, '_blank');
    }
  }
}

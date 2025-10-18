export class InvoiceListDto {
    id: string;
    invoiceNumber: string;
    invoiceDate = new Date();
    vendorName: string;
    vendorId: string;
    projectName: string;
    buJobNumber: string;
    grossAmount?: number;
    invoiceStatus: string;
    assignedUserFirstName: string;
    assignedUserLastName: string;
    assignedUserFullName: string;
}

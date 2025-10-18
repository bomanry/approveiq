namespace BLH.ApproveIQ.Application.DTOs;

public class InvoiceListDto
{
    public Guid Id { get; set; }
    public string? InvoiceNumber { get; set; }
    public DateTime? InvoiceDate { get; set; }
    public string? VendorName { get; set; }
    public string? VendorId { get; set; }
    public string? ProjectName { get; set; }         // From Project.Name
    public string? BuJobNumber { get; set; }
    public decimal? GrossAmount { get; set; }
    public string? InvoiceStatus { get; set; }
    
    // Assigned user info
    public string? AssignedUserFirstName { get; set; }  // From CurrentlyAssignedToUser.FirstName
    public string? AssignedUserLastName { get; set; }   // From CurrentlyAssignedToUser.LastName
    public string? AssignedUserFullName { get; set; }   // Computed field for frontend
}
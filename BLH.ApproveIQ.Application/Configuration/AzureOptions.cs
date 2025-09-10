using System.ComponentModel.DataAnnotations;

namespace BLH.ApproveIQ.API.Models;

public class AzureOptions
{
    [Required]
    public string ClientId { get; set; }
    [Required]
    public string ClientSecret { get; set; }
    [Required]
    public string TenantId { get; set; }
}
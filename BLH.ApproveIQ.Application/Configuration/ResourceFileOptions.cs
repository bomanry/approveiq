using System.ComponentModel.DataAnnotations;

namespace BLH.ApproveIQ.API.Models;

public class ResourceFileOptions
{
    [Required]
    public string ContainerName { get; set; }
}
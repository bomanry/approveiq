namespace BLH.ApproveIQ.Infrastructure.Services.Email.Configuration;

public class SendGridEmailConfiguration
{
    public string? ApiKey { get; set; }
    public string? FromAddress { get; set; }
    public string? FromName { get; set; }
}

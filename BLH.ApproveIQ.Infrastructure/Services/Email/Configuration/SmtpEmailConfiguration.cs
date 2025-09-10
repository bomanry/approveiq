namespace BLH.ApproveIQ.Infrastructure.Services.Email.Configuration;

public class SmtpEmailConfiguration
{
    public string? From { get; set; }
    public string? SmtpServer { get; set; }
    public int Port { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
}

using Microsoft.Extensions.Configuration;
using BLH.ApproveIQ.Application.Abstractions;
using BLH.ApproveIQ.Infrastructure.Services.Email.Concretes;
using BLH.ApproveIQ.Infrastructure.Services.Email.Configuration;

namespace BLH.ApproveIQ.Infrastructure.Services.Email;

internal sealed class EmailServiceFactory : IEmailServiceFactory
{
    private readonly IConfiguration _configuration;

    public EmailServiceFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public EmailService GetEmailService()
    {
        var emailService = _configuration["EmailService"];

        var smtpEmailConfig = _configuration
            .GetSection("SmtpEmailConfiguration")
            .Get<SmtpEmailConfiguration>();

        var sendGridEmailConfig = _configuration
            .GetSection("SendGridEmailConfiguration")
            .Get<SendGridEmailConfiguration>();
        
        var acsEmailConfig = _configuration
            .GetSection("ACSEmailConfiguration")
            .Get<ACSEmailConfiguration>();

        return emailService switch
        {
            // "Smtp" => new SmtpEmailService(smtpEmailConfig),
            // "SendGrid" => new SendGridEmailService(sendGridEmailConfig),
            "ACS" => new ACSEmailService(acsEmailConfig),
            _ => new ACSEmailService(acsEmailConfig),
        };
    }
}

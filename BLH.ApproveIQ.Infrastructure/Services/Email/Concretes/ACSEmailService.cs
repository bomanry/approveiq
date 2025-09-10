using BLH.ApproveIQ.Application.Abstractions;
using BLH.ApproveIQ.Domain.Entities;
using BLH.ApproveIQ.Domain.Identity.Models;
using BLH.ApproveIQ.Infrastructure.Services.Email.Configuration;
using BLH.ApproveIQ.Infrastructure.Services.Email.Templates;
using BLH.ApproveIQ.Persistence.Extensions;
using Azure;
using Azure.Communication.Email;

namespace BLH.ApproveIQ.Infrastructure.Services.Email.Concretes;

internal sealed class ACSEmailService : EmailService
{
    private readonly ACSEmailConfiguration _configuration;

    public ACSEmailService(ACSEmailConfiguration configuration)
    {
        _configuration = configuration;
    }
        
    // public override async Task SendIncompleteSessionReminderEmailAsync(string email, List<Session> Sessions, Guid TutorId, string TutorName, CancellationToken cancellationToken = default)
    // {
    //     var template = PastDueSessionResultsReminderEmailTemplate.GetTemplate(Sessions, TutorId, TutorName);
    //     
    //     await SendEmailToOneRecipientAsync(email, $"{AppName} - Assigned Sessions", template, cancellationToken);
    // }
    //
    // public override async Task SendAssignedTutorSessionEmailAsync(string email, List<Guid> SessionIds, Guid TutorId, CancellationToken cancellationToken = default)
    // {
    //     var template = AssignedSessionEmailTemplate.GetTemplate(SessionIds, TutorId);
    //     
    //     await SendEmailToOneRecipientAsync(email, $"{AppName} - Assigned Sessions", template, cancellationToken);
    // }
    //
    // public override async Task SendTestEmailAsync(string email, CancellationToken cancellationToken = default)
    // {
    //     var template =$"<!doctype html>" +
    //     $"<html lang='en-US'>" +
    //         $"<head>" +
    //             $"<meta content='text/html; charset=utf-8' http-equiv='Content-Type'/>" +
    //             $"<title>Test Email Template</title>" +
    //             $"<meta name='description' content='Assigned Session Email Template.'>" +
    //         $"</head>" +
    //         $"<body>" +
    //             $"<h1>TEST EMAIL</h1>" +
    //         $"</body>" +
    //     $"</html>";
    //     
    //     await SendEmailToOneRecipientAsync(email, $"{AppName} - TEST EMAIL", template, cancellationToken);
    // }
    //
    // private static string AttachImage(string body)
    // {
    //     //test this
    //     var currentDirectory = Directory.GetCurrentDirectory();
    //     var imagesDirectory = Path.Combine(currentDirectory, @"..\Spark.Infrastructure\Services\Email\Templates\Images");
    //     var imagePath = Path.Combine(imagesDirectory, "Sparkhound Logo.png");
    //
    //     using var fs = File.OpenRead(imagePath);
    //
    //     var base64Content = Convert.ToBase64String(fs.ReadAllBytes());
    //
    //     return body.Replace(EmailTemplateConstants.IMAGE_CONTENTID_REPLACE_TOKEN, $"data:image/png;base64,{base64Content}");
    // }
    //
    // private async Task SendEmailToOneRecipientAsync(string toAddress, string subject, string body, CancellationToken cancellationToken = default)
    // {
    //     try
    //     {
    //         var emailClient = new EmailClient(_configuration.ConnectionString);
    //
    //         var emailMessage = new EmailMessage(
    //             senderAddress: _configuration.FromAddress,
    //             content: new EmailContent(subject)
    //             {
    //                 Html = body
    //             },
    //             recipients: new EmailRecipients(new List<EmailAddress> { new EmailAddress(toAddress) }));
    //
    //         var emailSendOperation = await emailClient.SendAsync(
    //             WaitUntil.Completed,
    //             emailMessage,
    //             cancellationToken
    //         );
    //     }
    //     catch (Exception e)
    //     {
    //         Console.WriteLine(e);
    //     }
    // }
    //
    // private async Task SendEmailToMultipleRecipientsAsync(List<string> toAddresses, string subject, string body, CancellationToken cancellationToken = default)
    // {
    //     // var client = new SendGridClient(_configuration.ApiKey);
    //     // var from = new EmailAddress(_configuration.FromAddress);
    //     //
    //     // var toEmailList = new List<EmailAddress>();
    //     //
    //     // foreach (var email in toAddresses)
    //     // {
    //     //     toEmailList.Add(new EmailAddress(email));
    //     // }
    //     //
    //     // body = AttachImage(body);
    //     //
    //     // var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, toEmailList, subject, null, body);
    //     //
    //     // await client.SendEmailAsync(msg, cancellationToken);
    // }
}

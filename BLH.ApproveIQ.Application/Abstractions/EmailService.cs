using BLH.ApproveIQ.Domain.Entities;
using BLH.ApproveIQ.Domain.Identity.Models;

namespace BLH.ApproveIQ.Application.Abstractions;

public abstract class EmailService
{
    protected readonly string AppName = "BLH ApproveIQ";

    // public abstract Task SendTestEmailAsync(string email, CancellationToken cancellationToken = default);
    // public abstract Task SendAssignedTutorSessionEmailAsync(string email, List<Guid> sessionId, Guid tutorId, CancellationToken cancellationToken = default);
    // public abstract Task SendIncompleteSessionReminderEmailAsync(string email, List<Session> sessions, Guid tutorId, string tutorName, CancellationToken cancellationToken = default);
}

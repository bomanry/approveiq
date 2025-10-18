namespace BLH.ApproveIQ.Domain.Services;
public interface ICurrentUserService
{
    Guid? UserId { get; }
    bool UserExists { get; }
    string? Email { get; }
    string? FirstName { get; }
    string? LastName { get; }
}

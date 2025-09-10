namespace BLH.ApproveIQ.Domain.Exceptions;

public abstract class DomainException : Exception
{
    protected DomainException(string message)
        : base(message)
    {
    }
}
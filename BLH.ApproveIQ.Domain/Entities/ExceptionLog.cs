using BLH.ApproveIQ.Domain.Primitives;

namespace BLH.ApproveIQ.Domain.Entities;

public class ExceptionLog : Entity
{
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public string? Message { get; set; }
    public string? InnerExceptionStackTrace { get; set; }
    public string? InnerExceptionMessage { get; set; }
    public string? StackTrace { get; set; }

    public static ExceptionLog FromException(Exception e)
    {
        return new()
        {
            Message = e.Message,
            StackTrace = e.StackTrace,
            InnerExceptionMessage = e.InnerException?.Message,
            InnerExceptionStackTrace = e.InnerException?.StackTrace,
        };
    }
}

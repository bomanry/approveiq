using BLH.ApproveIQ.Domain.Shared;

namespace BLH.ApproveIQ.Domain.Errors;

public static class DomainErrors
{
    public static class ExampleErrors
    {
        public static readonly Func<Guid, Error> NotFound = id => new Error(
            "Example.NotFound",
            $"The Example Entry with the identifier {id} was not found.");

        public static readonly Func<Exception, Error> Generic = e => new Error(
            "Student.NotFound",
            $"{e}");

    }
}

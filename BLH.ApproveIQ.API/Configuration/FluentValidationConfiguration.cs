using FluentValidation;

namespace BLH.ApproveIQ.API.Configuration;

public static class FluentValidationConfiguration
{
    public static void ConfigureFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(
            BLH.ApproveIQ.Application.AssemblyReference.Assembly,
            includeInternalTypes: true);
    }
}

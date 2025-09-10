using Microsoft.AspNetCore.OData;

namespace BLH.ApproveIQ.API.Configuration;

public static class ControllersConfiguration
{
    public static void ConfigureControllers(this IServiceCollection services)
    {
        services
            .AddControllers()
            .AddApplicationPart(BLH.ApproveIQ.Presentation.AssemblyReference.Assembly)
            .AddOData(options =>
            {
                options.Filter().OrderBy().Select();
            });
    }
}

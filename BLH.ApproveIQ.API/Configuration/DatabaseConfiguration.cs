using Microsoft.EntityFrameworkCore;
using BLH.ApproveIQ.Persistence;
using BLH.ApproveIQ.Persistence.Interceptors;

namespace BLH.ApproveIQ.API.Configuration;

public static class DatabaseConfiguration
{
    public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("Default");

        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();

        services.AddSingleton<UpdateAuditableEntitiesInterceptor>();

        services.AddDbContext<ApplicationDbContext>(
            (sp, optionsBuilder) =>
            {
                var outboxInterceptor = sp.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>()!;
                var auditableInterceptor = sp.GetService<UpdateAuditableEntitiesInterceptor>()!;

                optionsBuilder
                .UseSqlServer(connectionString, builder =>
                {
                    builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                }).AddInterceptors(
                        outboxInterceptor,
                        auditableInterceptor);
            });
    }
}

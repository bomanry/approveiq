using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BLH.ApproveIQ.Persistence.Extensions;

public static class DatabaseExtensions
{
    public static async Task EnsureDbIsMigrated(this IApplicationBuilder builder)
    {
        using var serviceScope = builder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

        var services = serviceScope.ServiceProvider;

        var dbContext = services.GetRequiredService<ApplicationDbContext>();

        await dbContext.Database.MigrateAsync();
    }
}

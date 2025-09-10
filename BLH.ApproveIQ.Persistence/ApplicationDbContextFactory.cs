using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace BLH.ApproveIQ.Persistence
{
    /// <summary>
    /// Factory for creating ApplicationDbContext instances during design time (e.g., for EF Core migrations).
    /// </summary>
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // Build configuration
            // Adjust the base path as necessary to locate your appsettings.json,
            // typically in the startup project (e.g., API or Web Portal).
            // Going up two levels from Persistence project's directory to reach the solution level, then down into API.
            // Modify this path if your settings file is located elsewhere.
            string basePath = Path.Combine(Directory.GetCurrentDirectory(), "../BLH.ApproveIQ.API");
             if (!Directory.Exists(Path.Combine(basePath)))
             {
                 // Fallback if running from a different directory structure, adjust as needed.
                 // This assumes the command is run from the solution root.
                 basePath = Path.Combine(Directory.GetCurrentDirectory(), "BLH.ApproveIQ.API");
             }
             // If still not found, try relative path from Persistence project output
             if (!Directory.Exists(Path.Combine(basePath)))
             {
                  // Assumes bin/Debug/net8.0 is current dir during design time build
                 basePath = Path.Combine(Directory.GetCurrentDirectory(), "../../../../BLH.ApproveIQ.API");
             }


            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetFullPath(basePath))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            // Get connection string using the correct name "Default"
            var connectionString = configuration.GetConnectionString("Default");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Could not find a connection string named 'Default'.");
            }

            // Create options builder
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                // Add any other SQL Server specific options here if needed
            });

            // Return new context instance
            // Note: If your ApplicationDbContext constructor requires other dependencies,
            // this factory might need adjustments or you might need a parameterless constructor
            // specifically for design time (less ideal).
            // Assuming ApplicationDbContext has a constructor that accepts DbContextOptions<ApplicationDbContext>
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}

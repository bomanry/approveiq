using Azure.Identity;
using Microsoft.Extensions.Azure;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using BLH.ApproveIQ.API.Models;

namespace BLH.ApproveIQ.API.Configuration;

public static class AzureBlobConfiguration
{
    public static void ConfigureAzureBlob(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAzureClients(clientBuilder =>
        {
            string connectionString = configuration.GetConnectionString("BlobStorage")!;

            clientBuilder.AddBlobServiceClient(connectionString);
        });
    }
}

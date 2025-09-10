using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using BLH.ApproveIQ.API.Models;

namespace BLH.ApproveIQ.API.Configuration;

public static class GraphConfiguration
{
    public static void ConfigureGraph(this IServiceCollection services)
    {
        services.AddSingleton<GraphServiceClient>(sp =>
        {
            var azureOptions = sp.GetRequiredService<AzureOptions>();

            var scopes = new[] { "https://graph.microsoft.com/.default" };

            var clientId = azureOptions.ClientId;
            var tenantId = azureOptions.TenantId;
            var clientSecret = azureOptions.ClientSecret;

            var options = new ClientSecretCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
            };

            var clientSecretCredential = new ClientSecretCredential(
                tenantId, clientId, clientSecret, options);

            return new GraphServiceClient(clientSecretCredential, scopes);
        });
    }
}

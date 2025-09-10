using Newtonsoft.Json;

namespace BLH.ApproveIQ.Domain.Identity.Models;

[JsonObject("TokenOptions")]
public class Token
{
    [JsonProperty("Secret")]
    public string? Secret { get; set; }

    [JsonProperty("Issuer")]
    public string? Issuer { get; set; }

    [JsonProperty("Audience")]
    public string? Audience { get; set; }

    [JsonProperty("Expiry")]
    public int Expiry { get; set; }

    [JsonProperty("ValidateSecret")]
    public bool ValidateSecret { get; set; }

    [JsonProperty("ValidateIssuer")]
    public bool ValidateIssuer { get; set; }

    [JsonProperty("ValidateAudience")]
    public bool ValidateAudience { get; set; }

    [JsonProperty("RequireExpirationTime")]
    public bool RequireExpirationTime { get; set; }
}

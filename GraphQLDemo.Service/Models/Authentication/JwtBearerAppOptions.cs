namespace GraphQLDemo.Service.Models.Authentication;

public class JwtBearerAppOptions
{
    public string ValidIssuer { get; set; } = string.Empty;
    public string ValidAudience { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public string RefreshSecretKey { get; set; } = string.Empty;
    public int ExpirationHours { get; set; }
    public int RefreshExpirationHours { get; set; }

}
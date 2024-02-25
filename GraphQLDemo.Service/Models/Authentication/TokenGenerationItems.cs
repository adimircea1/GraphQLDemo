using System.Security.Claims;

namespace GraphQLDemo.Service.Models.Authentication;

public class TokenGenerationItems
{
    public string SecretToken { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int ExpirationHours { get; set; }
    public IEnumerable<Claim>? UserClaims { get; set;  } 
}
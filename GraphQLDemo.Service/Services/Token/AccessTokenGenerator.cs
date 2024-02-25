using System.Security.Claims;
using GraphQLDemo.Service.Abstractions.Tokens;
using GraphQLDemo.Service.Models.Authentication;

namespace GraphQLDemo.Service.Services.Token;

public class AccessTokenGenerator(JwtBearerAppOptions options, ITokenGenerator tokenGenerator)
    : IAccessTokenGenerator
{
    public string GenerateAccessToken(User user)
    {
        var claims = new List<Claim>
        {
            new("Id", user.Id.ToString()),
            new("Role", user.Role.ToString())
        };

        return tokenGenerator.GenerateToken(new TokenGenerationItems
        {
            Audience = options.ValidAudience,
            Issuer = options.ValidIssuer,
            SecretToken = options.SecretKey,
            ExpirationHours = options.ExpirationHours,
            UserClaims = claims
        });
    }
}
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using GraphQLDemo.Service.Abstractions.Tokens;
using GraphQLDemo.Service.Models.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace GraphQLDemo.Service.Services.Token;

public class RefreshTokenService(JwtBearerAppOptions options, ITokenGenerator tokenGenerator) : IRefreshTokenService
{
    public string GenerateRefreshToken() => tokenGenerator.GenerateToken(new TokenGenerationItems
    {
        Issuer = options.ValidIssuer,
        Audience = options.ValidAudience,
        ExpirationHours = options.RefreshExpirationHours,
        SecretToken = options.RefreshSecretKey,
        UserClaims = null
    });

    public async Task<bool> ValidateTokenAsync(string refreshToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            RequireExpirationTime = true,
            ValidIssuer = options.ValidIssuer,
            ValidAudience = options.ValidAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.RefreshSecretKey)),
            ClockSkew = TimeSpan.Zero
        };

        try
        {
             var result = await tokenHandler.ValidateTokenAsync(refreshToken, validationParameters);
        }
        catch
        {
            throw new Exception("The refresh token used for the request is not existent!");
        }

        return true;
    }
}
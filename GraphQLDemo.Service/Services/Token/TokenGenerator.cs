using System.IdentityModel.Tokens.Jwt;
using System.Text;
using GraphQLDemo.Service.Abstractions.Tokens;
using GraphQLDemo.Service.Models.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace GraphQLDemo.Service.Services.Token;

public class TokenGenerator : ITokenGenerator
{
    public string GenerateToken(TokenGenerationItems items)
    {
        //Create a security key using the secret token
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(items.SecretToken));
        
        //Create the user credentials and encrypt them with HmacSha256 algorithm
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        //Create the token itself
        var token = new JwtSecurityToken(
            items.Issuer,
            items.Audience,
            items.UserClaims,
            DateTime.UtcNow,
            DateTime.UtcNow.AddHours(items.ExpirationHours),
            credentials);

        //return the created token
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
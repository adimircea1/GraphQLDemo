using GraphQLDemo.Service.Models.Authentication;

namespace GraphQLDemo.Service.Abstractions.Tokens;

public interface ITokenGenerator
{
    public string GenerateToken(TokenGenerationItems items);
}
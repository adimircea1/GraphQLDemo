using GraphQLDemo.Service.Models.Authentication;

namespace GraphQLDemo.Service.Abstractions.Tokens;

public interface IAccessTokenGenerator
{
    public string GenerateAccessToken(User user);
}
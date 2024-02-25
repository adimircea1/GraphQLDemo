namespace GraphQLDemo.Service.Abstractions.Tokens;

public interface IRefreshTokenService
{
    public string GenerateRefreshToken();
    public Task<bool> ValidateTokenAsync(string refreshToken);
}
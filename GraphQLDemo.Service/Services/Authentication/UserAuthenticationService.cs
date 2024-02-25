using GraphQLDemo.Service.Abstractions;
using GraphQLDemo.Service.Abstractions.Authentication;
using GraphQLDemo.Service.Abstractions.Tokens;
using GraphQLDemo.Service.Extensions;
using GraphQLDemo.Service.Models.Authentication;
using Microsoft.AspNetCore.Identity.Data;

namespace GraphQLDemo.Service.Services.Authentication;

public class UserAuthenticationService(
    IAccessTokenGenerator accessTokenGenerator,
    IRefreshTokenService refreshTokenService,
    IDatabaseGenericRepository<User> userRepository)
    : IUserAuthenticationService
{
    public async Task RegisterAsync(UserRegisterRequest request)
    {
        var user = await userRepository.GetEntityByQueryAsync(user => user.Name == request.UserName);

        if (user != null)
        {
            throw new Exception("An user having this username already exists! Try to login instead!");
        }

        //Item1 = password hash | Item2 = password salt
        var userPasswordItems = request.Password.HashPassword();
        
        user = new User
        {
            Name = request.UserName,
            PasswordHash = userPasswordItems.Item1,
            PasswordSalt = userPasswordItems.Item2
        };

        await userRepository.AddEntityAsync(user);
    }

    public async Task<LoginResult> LoginAsync(UserLoginRequest request)
    {
        var user = await userRepository.GetEntityByQueryAsync(user => user.Name == request.UserName);

        if (user == null)
        {
            throw new Exception("User does not exist! Try to register first!");
        }

        if (!request.Password.VerifyPassword(user.PasswordHash, user.PasswordSalt))
        {
            throw new Exception("Passwords do not match!");
        }

        var refreshToken = refreshTokenService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;

        await userRepository.SaveChangesAsync();

        return new LoginResult
        {
            AccessToken = accessTokenGenerator.GenerateAccessToken(user),
            RefreshToken = refreshToken
        };
    }

    public async Task LogoutUserAsync(Guid userId)
    {
        var user = await userRepository.GetEntityAsync(userId);

        if (user == null)
        {
            throw new Exception("User not found!");
        }

        user.RefreshToken = null;

        await userRepository.SaveChangesAsync();
    }
}
using GraphQLDemo.Service.Models.Authentication;
using Microsoft.AspNetCore.Identity.Data;

namespace GraphQLDemo.Service.Abstractions.Authentication;

public interface IUserAuthenticationService
{
    public Task RegisterAsync(UserRegisterRequest request);
    public Task<LoginResult> LoginAsync(UserLoginRequest request);
    public Task LogoutUserAsync(Guid userId);
}
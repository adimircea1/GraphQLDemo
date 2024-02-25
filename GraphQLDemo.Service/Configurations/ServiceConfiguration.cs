using GraphQLDemo.Service.Abstractions;
using GraphQLDemo.Service.Abstractions.Authentication;
using GraphQLDemo.Service.Abstractions.Tokens;
using GraphQLDemo.Service.Services;
using GraphQLDemo.Service.Services.Authentication;
using GraphQLDemo.Service.Services.Token;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQLDemo.Service.Configurations;

public static class ServiceConfiguration
{
    public static void ConfigureService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IHumanService, HumanService>();
        serviceCollection.AddScoped<ITraitService, TraitService>();
        serviceCollection.AddScoped<IAccessTokenGenerator, AccessTokenGenerator>();
        serviceCollection.AddScoped<IRefreshTokenService, RefreshTokenService>();
        serviceCollection.AddScoped<ITokenGenerator, TokenGenerator>();
        serviceCollection.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
    }
}
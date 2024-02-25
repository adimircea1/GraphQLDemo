using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using GraphQLDemo.API.GraphQLSetup.GraphQLConfiguration;
using GraphQL;
using GraphQLDemo.Service.Models.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ServiceLifetime = GraphQL.DI.ServiceLifetime;

namespace GraphQLDemo.API.Configurations;

public static class PresentationConfiguration
{
    public static void ConfigurePresentation(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtBearerAppConfiguration = new JwtBearerAppOptions();
        configuration.GetSection("JwtBearerOptions").Bind(jwtBearerAppConfiguration);
        services.AddSingleton(jwtBearerAppConfiguration);
        
        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        }).AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });
        
        services.AddHttpContextAccessor();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                RequireExpirationTime = true,
                ValidIssuer = jwtBearerAppConfiguration.ValidIssuer,
                ValidAudience = jwtBearerAppConfiguration.ValidAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtBearerAppConfiguration.SecretKey)),
                ClockSkew = TimeSpan.Zero
            };
        });
        
        services.AddGraphQL(options => options
            .AddSchema<CustomSchema>(ServiceLifetime.Scoped)
            .AddGraphTypes(typeof(Startup).Assembly)
            .AddNewtonsoftJson()); 
        
    }
}
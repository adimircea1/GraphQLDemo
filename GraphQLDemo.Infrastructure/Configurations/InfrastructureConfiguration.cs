using GraphQLDemo.Infrastructure.Persistence;
using GraphQLDemo.Service.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQLDemo.Infrastructure.Configurations;

public static class InfrastructureConfiguration
{
    public static void ConfigureInfrastructure(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddDbContext<DataContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnectionString"));
        });

        serviceCollection.AddScoped<DataContext>();
        serviceCollection.AddScoped(typeof(IDatabaseGenericRepository<>), typeof(DatabaseGenericRepository<>));
    }
}
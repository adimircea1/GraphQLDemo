using System.Reflection;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQLDemo.Service.Extensions;

public static class GraphQLTypeDependencyInjector
{
    public static void InjectGraphQLTypes(this IServiceCollection services, Assembly assembly)
    {
        var graphQLTypes = assembly.GetTypes()
            .Where(type => typeof(IGraphType).IsAssignableFrom(type) && !type.IsInterface);

        foreach (var graphQlType in graphQLTypes)
        {
            services.AddScoped(graphQlType);
        }
    }
}
using GraphQL.Instrumentation;
using GraphQL.Types;
using GraphQLDemo.API.GraphQLSetup.GraphQLMutations;
using GraphQLDemo.API.GraphQLSetup.GraphQLQueries;
using GraphQLDemo.Service.Utils.GraphQLMiddlewares;

namespace GraphQLDemo.API.GraphQLSetup.GraphQLConfiguration;

public class CustomSchema : Schema
{
    public CustomSchema(IServiceProvider provider) : base(provider)
    {
        Query = provider.GetRequiredService<RootQuery>();
        Mutation = provider.GetRequiredService<RootMutation>();
        
        FieldMiddleware.Use(new PerformanceLoggingMiddleware());
    }
}
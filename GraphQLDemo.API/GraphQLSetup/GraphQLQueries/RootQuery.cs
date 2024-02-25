using GraphQL.Types;
using GraphQLDemo.Service.Abstractions;
using GraphQLDemo.Service.Abstractions.Authentication;
using GraphQLDemo.Service.Services.Authentication;

namespace GraphQLDemo.API.GraphQLSetup.GraphQLQueries;

public sealed class RootQuery 
    : ObjectGraphType, IQueryClass //ObjectGraphType implements IObjectGraphType 
{
    public RootQuery(IServiceProvider serviceProvider)
    {
        Field<HumanQuery>("HumanQuery").Resolve(_ => serviceProvider.GetRequiredService<HumanQuery>());
        Field<TraitQuery>("TraitQuery").Resolve(_ => serviceProvider.GetRequiredService<TraitQuery>());
        Field<AuthenticationQuery>("AuthenticationQuery").Resolve(_ => serviceProvider
            .GetRequiredService<IUserAuthenticationService>());
    }
}
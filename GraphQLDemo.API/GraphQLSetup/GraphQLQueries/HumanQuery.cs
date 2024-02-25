using GraphQL;
using GraphQL.Types;
using GraphQLDemo.API.GraphQLSetup.GraphQLTypes.OutputTypes;
using GraphQLDemo.Service.Abstractions;

namespace GraphQLDemo.API.GraphQLSetup.GraphQLQueries;

public sealed class HumanQuery : ObjectGraphType, IQueryClass
{
    //In the constructor define the fields of the query
    public HumanQuery(IHumanService humanService)
    {
        Field<ListGraphType<HumanOutputType>>("humans")
            .ResolveAsync(async _ => await humanService.GetHumansWithDependenciesAsync());

        Field<HumanOutputType>("human")
            .Arguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" })
            .ResolveAsync(async context =>
            {
                var id = context.GetArgument<Guid>("id");
                return await humanService.GetHumanWithDependencyAsync(id);
            });
    }
}
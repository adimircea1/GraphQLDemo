using GraphQL;
using GraphQL.Types;
using GraphQLDemo.API.GraphQLSetup.GraphQLTypes.OutputTypes;
using GraphQLDemo.Service.Abstractions;

namespace GraphQLDemo.API.GraphQLSetup.GraphQLQueries;

public sealed class TraitQuery : ObjectGraphType, IQueryClass
{
    public TraitQuery(ITraitService traitService )
    {
        Field<ListGraphType<TraitOutputType>>("traits")
            .ResolveAsync(async _ => await traitService.GetTraitsAsync());

        Field<TraitOutputType>("trait")
            .Arguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" })
            .ResolveAsync(async context =>
            {
                var id = context.GetArgument<Guid>("id");
                return await traitService.GetTraitAsync(id);
            });
    }
}
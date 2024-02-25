using GraphQL;
using GraphQL.Types;
using GraphQLDemo.API.GraphQLSetup.GraphQLTypes.InputTypes;
using GraphQLDemo.API.GraphQLSetup.GraphQLTypes.OutputTypes;
using GraphQLDemo.Service.Abstractions;
using GraphQLDemo.Service.Models;

namespace GraphQLDemo.API.GraphQLSetup.GraphQLMutations;

public sealed class TraitMutation : ObjectGraphType, IMutationClass
{
    public TraitMutation(ITraitService traitService)
    {
        Field<TraitOutputType>("addTrait")
            .Arguments(new QueryArgument<NonNullGraphType<TraitInputType>> { Name = "trait" })
            .ResolveAsync(async context =>
            {
                var trait = context.GetArgument<Trait>("trait");
                await traitService.AddTraitAsync(trait);
                return trait;
            });
        
        Field<StringGraphType>("deleteHuman")
            .Arguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "traitId" })
            .ResolveAsync(async context =>
            {
                var traitId = context.GetArgument<Guid>("traitId");
                await traitService.DeleteTraitAsync(traitId);
                return "Successfully deleted the human!";
            });
    }
}


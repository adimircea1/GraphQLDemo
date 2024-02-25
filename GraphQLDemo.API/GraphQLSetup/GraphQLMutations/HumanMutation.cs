using GraphQL;
using GraphQL.Types;
using GraphQLDemo.API.GraphQLSetup.GraphQLTypes.InputTypes;
using GraphQLDemo.API.GraphQLSetup.GraphQLTypes.OutputTypes;
using GraphQLDemo.Service.Abstractions;
using GraphQLDemo.Service.Models;

namespace GraphQLDemo.API.GraphQLSetup.GraphQLMutations;

public sealed class HumanMutation : ObjectGraphType, IMutationClass
{
    public HumanMutation(IHumanService humanService, IHttpContextAccessor httpContextAccessor)
    {
        Field<HumanOutputType>("addHuman")
            .Arguments(new QueryArgument<NonNullGraphType<HumanInputType>> { Name = "human" })
            .ResolveAsync(async context =>
            {
                var human = context.GetArgument<Human>("human");
                await humanService.AddHumanAsync(human);
                return human;
            });

        Field<HumanOutputType>("assignTraitsToHuman")
            .Arguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "humanId" })
            .Arguments(new QueryArgument<ListGraphType<IdGraphType>> { Name = "traitIds" })
            .ResolveAsync(async context =>
            {
                var humanId = context.GetArgument<Guid>("humanId");
                var traitIds = context.GetArgument<List<Guid>>("traitIds");

                if (traitIds.Count != 0)
                {
                    await humanService.AssignTraitsToHuman(humanId, traitIds);
                }

                return await humanService.GetHumanWithDependencyAsync(humanId);
            });

        Field<StringGraphType>("deleteHuman")
            .Arguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "humanId" })
            .ResolveAsync(async context =>
            {
                var humanId = context.GetArgument<Guid>("humanId");
                await humanService.DeleteHumanAsync(humanId);
                return "Successfully deleted the human!";
            });

        Field<StringGraphType>("updateHuman")
            .Arguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "humanId" })
            .Arguments(new QueryArgument<HumanUpdateInputType> { Name = "humanInput" })
            .Arguments(new QueryArgument<ListGraphType<StringGraphType>> { Name = "propertiesToChange"})
            .ResolveAsync(async context =>
            {
                var id = context.GetArgument<Guid>("humanId");
                var updatedHuman = context.GetArgument<Human>("humanInput");
                var propertiesToChange = context.GetArgument<List<string>>("propertiesToChange");
                await humanService.UpdateHumanAsync(id, updatedHuman, propertiesToChange);
                return "Successfully updated human!";
            });
    }
}


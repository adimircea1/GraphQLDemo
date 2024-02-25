using GraphQL.Types;
using GraphQLDemo.Service.Abstractions;

namespace GraphQLDemo.API.GraphQLSetup.GraphQLMutations;

public sealed class RootMutation 
    : ObjectGraphType, IMutationClass
{
    public RootMutation(IServiceProvider serviceProvider)
    {
        Field<HumanMutation>("HumanMutation").Resolve(_ => serviceProvider.GetRequiredService<HumanMutation>());
        Field<TraitMutation>("TraitMutation").Resolve(_ => serviceProvider.GetRequiredService<TraitMutation>());
    }
}
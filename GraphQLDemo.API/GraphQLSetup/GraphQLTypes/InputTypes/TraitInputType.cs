using GraphQL.Types;
using GraphQLDemo.Service.Models;

namespace GraphQLDemo.API.GraphQLSetup.GraphQLTypes.InputTypes;

public sealed class TraitInputType : InputObjectGraphType<Trait>
{
    public TraitInputType()
    {
        Name = "TraitInput";
        Field(trait => trait.Name, nullable: false).Description("Trait Name");
        Field(trait => trait.Consequence, nullable: false).Description("Trait Consequence");
    }
}
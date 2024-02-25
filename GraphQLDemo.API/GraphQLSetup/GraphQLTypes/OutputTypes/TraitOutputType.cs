using GraphQL.Types;
using GraphQLDemo.Service.Models;

namespace GraphQLDemo.API.GraphQLSetup.GraphQLTypes.OutputTypes;

public sealed class TraitOutputType : ObjectGraphType<Trait>
{
    public TraitOutputType()
    {
        Name = "Trait";
        Description = "Query for trait type";
        Field(trait => trait.Id, nullable: false).Description("Trait Id");
        Field(trait => trait.Name, nullable: false).Description("Trait Name");
        Field(trait => trait.Consequence, nullable: false).Description("Trait Consequence");
    }
}
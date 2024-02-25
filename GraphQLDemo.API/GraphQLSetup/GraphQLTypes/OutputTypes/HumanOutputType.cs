using GraphQL.Types;
using GraphQLDemo.Service.Abstractions;
using GraphQLDemo.Service.Models;

namespace GraphQLDemo.API.GraphQLSetup.GraphQLTypes.OutputTypes;

public sealed class HumanOutputType : ObjectGraphType<Human>
{
    //Define the fields for this type
    public HumanOutputType(IHumanService humanService)
    {
        Name = "Human";
        Description = "Query for human type";
        Field(human => human.Id, nullable: false).Description("Human Id");
        Field(human => human.Age, nullable: false).Description("Human Age");
        Field(human => human.Gender, nullable: false).Description("Human Gender");
        Field(human => human.Name, nullable: false).Description("Human Name");
        Field<ListGraphType<TraitOutputType>>(Name = "traits").Description("Human traits");
    }
}
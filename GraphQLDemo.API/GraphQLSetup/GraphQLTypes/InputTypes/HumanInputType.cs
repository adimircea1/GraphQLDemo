using GraphQL.Types;
using GraphQLDemo.Service.Models;

namespace GraphQLDemo.API.GraphQLSetup.GraphQLTypes.InputTypes;

public sealed class HumanInputType : InputObjectGraphType<Human>
{
    public HumanInputType()
    {
        Name = "HumanInput";
        Field(human => human.Age, nullable: false).Description("Human Age");
        Field(human => human.Gender, nullable: false).Description("Human Gender");
        Field(human => human.Name, nullable: false).Description("Human Name");
    }
}
using GraphQL.Types;
using GraphQLDemo.Service.Models;

namespace GraphQLDemo.API.GraphQLSetup.GraphQLTypes.InputTypes;

public sealed class HumanUpdateInputType : InputObjectGraphType<Human>
{
    public HumanUpdateInputType()
    {
        Name = "HumanUpdateInput";
        Field(human => human.Age, nullable: true).Description("Human Age");
        Field(human => human.Gender, nullable: true).Description("Human Gender");
        Field(human => human.Name, nullable: true).Description("Human Name");
    }
}
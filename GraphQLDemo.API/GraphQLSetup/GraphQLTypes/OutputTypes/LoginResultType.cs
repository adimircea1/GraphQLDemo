using GraphQL.Types;
using GraphQLDemo.Service.Models.Authentication;

namespace GraphQLDemo.API.GraphQLSetup.GraphQLTypes.OutputTypes;

public sealed class LoginResultType : ObjectGraphType<LoginResult>
{
    public LoginResultType()
    {
        Field(result => result.AccessToken).Description("Access token");
        Field(result => result.RefreshToken).Description("Refresh token");
    }
}
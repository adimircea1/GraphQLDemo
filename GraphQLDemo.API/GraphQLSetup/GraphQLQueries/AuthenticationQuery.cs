using System.Security.Claims;
using GraphQL;
using GraphQL.Types;
using GraphQLDemo.API.GraphQLSetup.GraphQLTypes.OutputTypes;
using GraphQLDemo.Service.Abstractions;
using GraphQLDemo.Service.Abstractions.Authentication;
using GraphQLDemo.Service.Models.Authentication;

namespace GraphQLDemo.API.GraphQLSetup.GraphQLQueries;

public sealed class AuthenticationQuery : ObjectGraphType, IQueryClass
{
    public AuthenticationQuery(IUserAuthenticationService userAuthenticationService)
    {
        Field<StringGraphType>("register")
            .Arguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "username" })
            .Arguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "password" })
            .Arguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "confirmPassword" })
            .ResolveAsync(async context =>
            {
                await userAuthenticationService.RegisterAsync(new UserRegisterRequest
                {
                    UserName = context.GetArgument<string>("username"),
                    Password = context.GetArgument<string>("password"),
                    ConfirmPassword = context.GetArgument<string>("confirmPassword")
                });

                return "Successfully registered user!";
            });

        Field<LoginResultType>("login")
            .Arguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "username" })
            .Arguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "password" })
            .ResolveAsync(async context =>
            {
                try
                {
                    return await userAuthenticationService.LoginAsync(new UserLoginRequest
                    {
                        UserName = context.GetArgument<string>("username"),
                        Password = context.GetArgument<string>("password")
                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            });

        Field<StringGraphType>("logout")
            .ResolveAsync(async context =>
            {
                var userIdClaim = context.UserContext;
                return string.Empty;
            });
    }
}
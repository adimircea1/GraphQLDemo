namespace GraphQLDemo.Service.Models.Authentication;

public class UserLoginRequest
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
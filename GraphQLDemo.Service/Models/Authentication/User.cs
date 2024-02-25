using GraphQLDemo.Service.Abstractions;

namespace GraphQLDemo.Service.Models.Authentication;

public class User : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string PasswordSalt { get; set; } = string.Empty;
    public Role Role { get; set; } = Role.Common;
    public string? RefreshToken { get; set; } 
}
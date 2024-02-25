using GraphQLDemo.Service.Abstractions;

namespace GraphQLDemo.Service.Models;

public class Human : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public int Age { get; set; }
    public Gender Gender { get; set; }
    public List<Trait> Traits { get; set; } = [];
    public string? RefreshToken { get; set; }
}
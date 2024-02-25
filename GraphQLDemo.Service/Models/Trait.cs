using GraphQLDemo.Service.Abstractions;

namespace GraphQLDemo.Service.Models;

public class Trait : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    //The consequence of someone having this trait
    public Consequence Consequence { get; set; }
    public List<Human> Humans { get; set; } = [];
}
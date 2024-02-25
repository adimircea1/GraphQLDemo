using GraphQLDemo.Service.Models;

namespace GraphQLDemo.Service.Abstractions;

public interface ITraitService
{
    public Task<Trait> GetTraitAsync(Guid id);
    public Task<List<Trait>> GetTraitsAsync();
    public Task AddTraitAsync(Trait trait);
    public Task DeleteTraitAsync(Guid id);
    public Task UpdateTraitAsync(Guid id, Trait updatedTrait, List<string> propertiesToChange);
    public Task QueueAddTraitAsync(Trait trait);
    public Task QueueDeleteTraitAsync(Guid id);
    public Task QueueUpdateTraitAsync(Guid id, Trait updatedTrait, List<string> propertiesToChange);
    public Task SaveChangesAsync();
}
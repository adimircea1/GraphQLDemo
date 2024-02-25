using GraphQLDemo.Service.Models;

namespace GraphQLDemo.Service.Abstractions;

public interface IHumanService
{
    public Task<Human> GetHumanAsync(Guid id);
    public Task<List<Human>> GetHumansAsync();
    public Task<List<Human>> GetHumansWithDependenciesAsync();
    public Task<Human> GetHumanWithDependencyAsync(Guid id);
    public Task<List<Trait>> GetHumanTraitsAsync(Guid id);
    public Task UpdateHumanAsync(Guid id, Human updatedHuman, List<string> propertiesToChange);
    public Task AssignTraitToHuman(Guid humanId, Guid traitId);
    public Task AssignTraitsToHuman(Guid humanId, List<Guid> traitIds);
    public Task AddHumanAsync(Human human);
    public Task DeleteHumanAsync(Guid id);
    public Task QueueAddHumanAsync(Human human);
    public Task QueueDeleteHumanAsync(Guid id);
    public Task QueueAssignTraitToHuman(Guid humanId, Guid traitId);
    public Task QueueAssignTraitsToHuman(Guid humanId, List<Guid> traitIds);
    public Task QueueUpdateHumanAsync(Guid id, Human updatedHuman, List<string> propertiesToChange);
    public Task SaveChangesAsync();
}
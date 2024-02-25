using GraphQLDemo.Service.Abstractions;
using GraphQLDemo.Service.Models;

namespace GraphQLDemo.Service.Services;

public class HumanService(
    IDatabaseGenericRepository<Human> humanRepository,
    ITraitService traitService) : IHumanService
{
    public async Task<Human> GetHumanAsync(Guid id)
    {
        return await humanRepository.GetEntityAsync(id) ?? throw new Exception("Human not found!");
    }

    public async Task<List<Human>> GetHumansAsync()
    {
        return await humanRepository.GetEntitiesAsync();
    }

    public async Task<List<Human>> GetHumansWithDependenciesAsync()
    {
        return await humanRepository.GetEntitiesWithDependencyAsync(human => human.Traits);
    }

    public async Task<Human> GetHumanWithDependencyAsync(Guid id)
    {
        return await humanRepository.GetEntityByQueryWithDependencyAsync(human => human.Id == id,
            human => human.Traits) ?? throw new Exception("Human not found!");
    }

    public async Task<List<Trait>> GetHumanTraitsAsync(Guid id)
    {
        var human = await humanRepository.GetEntityByQueryWithDependencyAsync(human => human.Id == id,
            human => human.Traits);

        return human == null
            ? []
            : human.Traits;
    }

    public async Task UpdateHumanAsync(Guid id, Human updatedHuman, List<string> propertiesToChange)
    {
        await QueueUpdateHumanAsync(id, updatedHuman, propertiesToChange);
        await SaveChangesAsync();
    }

    public async Task AssignTraitToHuman(Guid humanId, Guid traitId)
    {
        await QueueAssignTraitToHuman(humanId, traitId);
        await SaveChangesAsync();
    }

    public async Task AssignTraitsToHuman(Guid humanId, List<Guid> traitIds)
    {
        await QueueAssignTraitsToHuman(humanId, traitIds);
        await SaveChangesAsync();
    }
    
    public async Task AddHumanAsync(Human human)
    {
        await QueueAddHumanAsync(human);
        await SaveChangesAsync();
    }

    public async Task DeleteHumanAsync(Guid id)
    {
        await QueueDeleteHumanAsync(id);
        await SaveChangesAsync();
    }

    public async Task QueueAddHumanAsync(Human human)
    {
        await humanRepository.AddEntityAsync(human);
    }

    public async Task QueueDeleteHumanAsync(Guid id)
    {
        var humanToDelete = await GetHumanAsync(id);
        humanRepository.DeleteEntity(humanToDelete);
    }

    public async Task QueueAssignTraitToHuman(Guid humanId, Guid traitId)
    {
        var human = await humanRepository.GetEntityByQueryWithDependencyAsync(human => human.Id == humanId,
            human => human.Traits);

        if (human == null)
        {
            throw new Exception("Human not found!");
        }
        
        var trait = await traitService.GetTraitAsync(traitId);

        if (human.Traits.Contains(trait))
        {
            return;
        }
        
        human.Traits.Add(trait);
    }

    public async Task QueueAssignTraitsToHuman(Guid humanId, List<Guid> traitIds)
    {
        var human = await humanRepository.GetEntityByQueryWithDependencyAsync(human => human.Id == humanId,
            human => human.Traits);
        
        if (human == null)
        {
            throw new Exception("Human not found!");
        }
        
        foreach (var traitId in traitIds)
        {
            var trait = await traitService.GetTraitAsync(traitId);

            if (human.Traits.Contains(trait))
            {
                continue;
            }
            
            human.Traits.Add(trait);
        }
    }

    public async Task QueueUpdateHumanAsync(Guid id, Human updatedHuman, List<string> propertiesToChange)
    {
        var outdatedHuman = await GetHumanAsync(id);
        humanRepository.UpdateEntity(outdatedHuman, updatedHuman, propertiesToChange);
    }

    public async Task SaveChangesAsync()
    {
        await humanRepository.SaveChangesAsync();
    }
}
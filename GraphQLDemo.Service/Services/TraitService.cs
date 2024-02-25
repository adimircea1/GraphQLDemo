using GraphQLDemo.Service.Abstractions;
using GraphQLDemo.Service.Models;

namespace GraphQLDemo.Service.Services;

public class TraitService(IDatabaseGenericRepository<Trait> traitRepository) : ITraitService
{
    public async Task<Trait> GetTraitAsync(Guid id)
    {
        return await traitRepository.GetEntityAsync(id) ?? throw new Exception("Trait not found!");
    }

    public async Task<List<Trait>> GetTraitsAsync()
    {
        return await traitRepository.GetEntitiesAsync();
    }

    public async Task AddTraitAsync(Trait trait)
    {
        await QueueAddTraitAsync(trait);
        await SaveChangesAsync();
    }

    public async Task DeleteTraitAsync(Guid id)
    {
        await QueueDeleteTraitAsync(id);
        await SaveChangesAsync();
    }

    public async Task UpdateTraitAsync(Guid id, Trait updatedTrait, List<string> propertiesToChange)
    {
        await QueueUpdateTraitAsync(id, updatedTrait, propertiesToChange);
        await SaveChangesAsync();
    }

    public async Task QueueAddTraitAsync(Trait trait)
    {
        await traitRepository.AddEntityAsync(trait);
    }

    public async Task QueueDeleteTraitAsync(Guid id)
    {
        var traitToDelete = await GetTraitAsync(id);
        traitRepository.DeleteEntity(traitToDelete);
    }

    public async Task QueueUpdateTraitAsync(Guid id, Trait updatedTrait, List<string> propertiesToChange)
    {
        var outdatedTrait = await GetTraitAsync(id);
        traitRepository.UpdateEntity(outdatedTrait, updatedTrait, propertiesToChange);
    }

    public async Task SaveChangesAsync()
    {
        await traitRepository.SaveChangesAsync();
    }
}
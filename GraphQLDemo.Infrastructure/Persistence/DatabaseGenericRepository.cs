using System.Linq.Expressions;
using GraphQLDemo.Service.Abstractions;
using GraphQLDemo.Service.Extensions;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo.Infrastructure.Persistence;

public class DatabaseGenericRepository<TEntity>(DataContext context) : IDatabaseGenericRepository<TEntity>
    where TEntity : class, IEntity
{
    private readonly DbSet<TEntity> _entitySet = context.Set<TEntity>();

    public async Task<TEntity?> GetEntityByQueryAsync(Expression<Func<TEntity, bool>> query)
    {
        return await _entitySet.Where(query).FirstOrDefaultAsync();
    }

    public async Task<TEntity?> GetEntityAsync(Guid id)
    {
        return await _entitySet.FindAsync(id);
    }

    public async Task<List<TEntity>> GetEntitiesAsync()
    {
        return await _entitySet.ToListAsync();
    }

    public async Task<List<TEntity>> GetEntitiesWithDependencyAsync<TProperty>(
        Expression<Func<TEntity, TProperty>> dependency)
    {
        return await _entitySet.Include(dependency).ToListAsync();
    }

    public async Task<TEntity?> GetEntityByQueryWithDependencyAsync<TProperty>(Expression<Func<TEntity, bool>> query,
        Expression<Func<TEntity, TProperty>> dependency)
    {
        return await _entitySet.Include(dependency).FirstOrDefaultAsync(query);
    }

    public async Task AddEntityAsync(TEntity entity)
    {
        await _entitySet.AddAsync(entity);
    }

    public void UpdateEntity(TEntity outdatedEntity, TEntity updatedEntity, List<string> propertiesToChange)
    {
        outdatedEntity.UpdateEntity(updatedEntity, propertiesToChange);
    }

    public void DeleteEntity(TEntity entity)
    {
        _entitySet.Remove(entity);
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}
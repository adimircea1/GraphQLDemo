using System.Linq.Expressions;

namespace GraphQLDemo.Service.Abstractions;

public interface IDatabaseGenericRepository<TEntity> where TEntity : IEntity
{
    public Task<TEntity?> GetEntityByQueryAsync(Expression<Func<TEntity, bool>> query);
    public Task<TEntity?> GetEntityAsync(Guid id);
    public Task<List<TEntity>> GetEntitiesAsync();
    public Task<List<TEntity>> GetEntitiesWithDependencyAsync<TProperty>(
        Expression<Func<TEntity, TProperty>> dependency);
    public Task<TEntity?> GetEntityByQueryWithDependencyAsync<TProperty>(Expression<Func<TEntity, bool>> query,
        Expression<Func<TEntity, TProperty>> dependency);
    public void DeleteEntity(TEntity entity);
    public Task AddEntityAsync(TEntity entity);
    public void UpdateEntity(TEntity outdatedEntity, TEntity updatedEntity, List<string> propertiesToChange);
    public Task SaveChangesAsync();

}
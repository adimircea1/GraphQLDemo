using GraphQLDemo.Service.Abstractions;

namespace GraphQLDemo.Service.Extensions;

public static class EntityExtension
{
    public static void UpdateEntity<TEntity>(this TEntity entityToUpdate, TEntity updatedEntity,
        IEnumerable<string> propertiesToChange) where TEntity : class, IEntity
    {
        foreach (var propertyName in propertiesToChange)
        {
            var propertyInfo = typeof(TEntity).GetProperties()
                .FirstOrDefault(property => string.Equals(propertyName, property.Name, StringComparison.InvariantCultureIgnoreCase));


            if (propertyInfo is null)
            {
                continue;
            }
            
            var updatedEntityPropertyValue = propertyInfo.GetValue(updatedEntity);
            
            propertyInfo.SetValue(entityToUpdate, updatedEntityPropertyValue);
        }
    }
}
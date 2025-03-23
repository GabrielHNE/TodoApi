using Todo.Domain.Entities;

namespace Todo.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        ValueTask<TEntity> SaveAsync(TEntity entity);
        ValueTask<TEntity> GetByIdAsync(long? id);
        ValueTask<IEnumerable<TEntity>> GetAllAsync();
        Task DeleteAsync(long? id);
        ValueTask<bool> ExistsByIdAsync(long? id);
    }
}
using UserManagementService.Shared.Core.Aggregate;

namespace UserManagementService.Shared.Core.Interfaces;
public interface IRepository<TEntity> where TEntity : BaseEntity
{
    TEntity FindById(Guid id);
    Task<TEntity> FindOneAsync(ISpecification<TEntity> spec);
    Task<List<TEntity>> FindAsync(ISpecification<TEntity> spec);
    Task<TEntity> AddAsync(TEntity entity);
    Task RemoveAsync(TEntity entity);
}
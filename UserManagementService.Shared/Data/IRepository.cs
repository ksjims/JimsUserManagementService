using UserManagementService.Shared.Specification;

namespace UserManagementService.Shared.Data;
public interface IRepository<TEntity> where TEntity : BaseEntity
{
    TEntity FindById(Guid id);
    Task<TEntity> FindOneAsync(ISpecification<TEntity> spec);
    Task<List<TEntity>> FindAsync(ISpecification<TEntity> spec);
    Task<TEntity> AddAsync(TEntity entity);
    Task RemoveAsync(TEntity entity);
}
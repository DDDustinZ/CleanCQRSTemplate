using System.Linq.Expressions;

namespace COMPANY_NAME.PRODUCT.Core.Abstracts;

public interface IGenericRepository<TEntity, in TId> where TEntity : IEntity<TId>
{
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken ct);
    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter, CancellationToken ct);
    Task<TEntity?> GetByIdAsync(TId id, CancellationToken ct);
    Task<TEntity?> GetOneAsync(Expression<Func<TEntity, bool>> filter, CancellationToken ct);
    void Add(TEntity entity);
    void AddAll(IEnumerable<TEntity> entities);
    void Delete(TEntity entity);
    void DeleteAll(IEnumerable<TEntity> entities);
    void Update(TEntity entity);
    void UpdateAll(IEnumerable<TEntity> entities);
}